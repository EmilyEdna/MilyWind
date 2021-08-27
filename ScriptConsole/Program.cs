using IdGen;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ScriptConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new ContextDb();
            var ids = db.Context().Queryable<AbpTenant>().Where(t => t.IsActive == true).Select(t => t.Id).ToList();
            List<SmsTemplate> sms = new List<SmsTemplate>();
            ids.ForEach(item =>
            {
                sms.Add(new SmsTemplate
                {
                    CreatorUserId=0,
                    CreationTime=DateTime.Now,
                    TenantId= item,
                    TemplateCode = "00005",
                    IsActive = false,
                    TemplateName= "计次卡购买",
                    TemplateContent = "尊敬的【会员名称】会员，您已购买【计次卡名称】计次卡，包含【计次次数】次消费次数，有效天数为【有效天数】天"
                });
                sms.Add(new SmsTemplate
                {
                    CreatorUserId = 0,
                    CreationTime = DateTime.Now,
                    TenantId = item,
                    TemplateCode = "00006",
                    IsActive = false,
                    TemplateName = "计次卡消费",
                    TemplateContent = "尊敬的【会员名称】，您于【系统日期】【系统时间】在【机构名称】消费【计次卡名称】【消费次数】次，计次卡剩余次数【剩余次数】"
                });
                sms.Add(new SmsTemplate
                {
                    CreatorUserId = 0,
                    CreationTime = DateTime.Now,
                    TenantId = item,
                    TemplateCode = "00007",
                    IsActive = false,
                    TemplateName = "计次卡延期",
                    TemplateContent = "尊敬的【会员名称】，您于【系统日期】【系统时间】在【机构名称】将【计次卡名称】延期至【有效期至】"
                });
            });
            sms.ForEach(item => {
                item.Sort = item.Id = CreateGenId();
                Thread.Sleep(50);
                Console.WriteLine(item.Sort);
            });
            var dbc = db.Context();
            try
            {
                dbc.BeginTran();
                dbc.Insertable(sms).ExecuteCommand();
                dbc.CommitTran();
            }
            catch (Exception)
            {
                Console.WriteLine("执行错误，正在回滚");
                dbc.RollbackTran();
            }
            Console.WriteLine("执行完成");
            Console.ReadLine();
        }

        public static long CreateGenId()
        {
            DateTime epoch = new DateTime(2020, 4, 1, 0, 0, 0, DateTimeKind.Utc);
            IdStructure structure = new IdStructure(41, 10, 12);
            IdGeneratorOptions options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));
            int Seed = new Random(Guid.NewGuid().GetHashCode()).Next(0, 1024);
            IdGenerator IdGen = new IdGenerator(Seed, options);
            long GenId = IdGen.CreateId();
            return GenId;
        }
    }
}
