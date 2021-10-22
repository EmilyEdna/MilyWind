using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class LogInput
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public int? LogLv { get; set; }
        /// <summary>
        /// 日志环境
        /// </summary>
        public int LogEnv { get; set; } = 0;
        /// <summary>
        /// 所属服务
        /// </summary>
        public string SystemService { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
