using Mily.Wind.LogPlugin.DTO;
using Mily.Wind.LogPlugin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.LogPlugin
{
    public class Log : ILog
    {
        public void WriteLog(string Message, LogLevelEnum Type = LogLevelEnum.Info, object Param = null, Exception exception = null)
        {
            if (Param != null)
                Message = $"【{Message}】-参数【{Param}】";

            MilyLogOption Logopts = LogIoc.Get<MilyLogOption>(nameof(MilyLogOption));
            if (Logopts == null) throw new Exception("未注册日志组件，请先注册后在使用!");
            List<LogWriteInput> inputs = new List<LogWriteInput>
            {
                new LogWriteInput
                {
                    CreatedTime = DateTime.Now,
                    ErrorMsg = Message,
                    LogLv = Type,
                    StackTrace = exception?.StackTrace,
                    LogEnv =Logopts.IsDevelopment? LogEnvEnum.Dev:LogEnvEnum.Pro,
                    SystemService=Logopts.SystemService
                }
            };
            IHttpMultiClient.HttpMulti.AddNode(opt =>
            {
                opt.ReqType = MultiType.POST;
                opt.JsonParam = inputs.ToJson();
                opt.NodePath = $"{Logopts.Scheme}://{Logopts.Host}:{Logopts.Port}{MilyLogOption.Route}";
            }).Build().RunString();
        }
    }
}
