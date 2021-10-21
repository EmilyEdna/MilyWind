using Mily.Wind.LogPlugin.DTO;
using Mily.Wind.LogPlugin.Enums;
using System;
using System.Collections;
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
        private static ArrayList inputs = new ArrayList();
        public void WriteLog(string Message, LogLevelEnum Type = LogLevelEnum.Info, object Param = null, Exception exception = null)
        {
            if (Param != null)
                Message = $"【{Message}】-参数【{Param}】";

            MilyLogOption opts = LogIoc.Get<MilyLogOption>(nameof(MilyLogOption));
            if (opts == null) throw new Exception("未注册日志组件，请先注册后在使用!");
            if (opts.UseBatchLog)
            {
                if (inputs.Count == 10)
                {
                    Request(inputs);
                    inputs.Clear();
                }
                else
                {
                    LogWriteInput input = new LogWriteInput
                    {
                        CreatedTime = DateTime.Now,
                        ErrorMsg = Message,
                        LogLv = Type,
                        StackTrace = exception?.StackTrace,
                        LogEnv = opts.IsDevelopment ? LogEnvEnum.Dev : LogEnvEnum.Pro,
                        SystemService = opts.SystemService,
                        TraceId = MilyLogOption.TraceNode
                    };
                    inputs.Add(input);
                }
            }
            else
            {
                List<LogWriteInput> input = new List<LogWriteInput>() 
                {
                  new LogWriteInput{
                   CreatedTime = DateTime.Now,
                     ErrorMsg = Message,
                     LogLv = Type,
                     StackTrace = exception?.StackTrace,
                     LogEnv = opts.IsDevelopment ? LogEnvEnum.Dev : LogEnvEnum.Pro,
                     SystemService = opts.SystemService,
                     TraceId = MilyLogOption.TraceNode
                  }
                };
                Request(input);
            }

        }

        private void Request(object ojb)
        {
            MilyLogOption opts = LogIoc.Get<MilyLogOption>(nameof(MilyLogOption));
            IHttpMultiClient.HttpMulti.AddNode(opt =>
            {
                opt.ReqType = MultiType.POST;
                opt.JsonParam = ojb.ToJson();
                opt.NodePath = $"{opts.Url}{MilyLogOption.Route}";
            }).Build().RunString();
        }
    }
}
