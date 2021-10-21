using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin
{
    public class MilyLogOption
    {
        /// <summary>
        /// 异常识别码
        /// </summary>
        public static string TraceNode { get; set; }
        internal const string Route = "/Log/WriteLog";
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 所属服务
        /// </summary>
        public string SystemService { get; set; }
        /// <summary>
        /// 环境
        /// </summary>
        public bool IsDevelopment { get; set; }
        /// <summary>
        /// 是否启用批量上传日志
        /// </summary>
        public bool UseBatchLog { get; set; } = false;
        /// <summary>
        /// 每次提交数量
        /// </summary>
        public int BatchCount { get; set; } = 10;
    }
}
