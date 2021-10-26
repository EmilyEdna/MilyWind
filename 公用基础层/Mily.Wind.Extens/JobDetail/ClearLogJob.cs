using Mily.Wind.QuartzPlugin;
using Mily.Wind.VMod.Mogo;
using Quartz;
using System;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Extens.JobDetail
{
    public class ClearLogJob : JobBasic
    {
        public override Task Execute(IJobExecutionContext context)
        {
            var Time = DateTime.Parse(DateTime.Now.AddDays(-7).ToFmtDate(3, "yyyy-MM-dd"));
            MongoDbCaches.DeleteMany<LogMogoViewModel>(t => t.CreatedTime < Time);
            return base.Execute(context);
        }
    }
}
