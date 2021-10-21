using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class PluginInput
    {
        /// <summary>
        /// 插件别名
        /// </summary>
        public string PluginAlias { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEable { get; set; } 
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
