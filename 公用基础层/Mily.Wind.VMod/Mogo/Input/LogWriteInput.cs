using Mily.Wind.VMod.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class LogWriteInput
    {
        /// <summary>
        /// 堆栈
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevelEnum LogLv { get; set; }
        /// <summary>
        /// 日志链
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 所属服务
        /// </summary>
        public string SystemService { get; set; }
        /// <summary>
        /// 系统环境
        /// </summary>
        public EnvEnum LogEnv { get; set; }
    }
}
