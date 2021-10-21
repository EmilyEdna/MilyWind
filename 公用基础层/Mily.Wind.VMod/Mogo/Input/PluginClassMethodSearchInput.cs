using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class PluginClassMethodSearchInput
    {
        /// <summary>
        /// 插件Id
        /// </summary>
        public string PluginId { get; set; }
        /// <summary>
        /// 查询类型 1查询类2查询方法
        /// </summary>
        public int SearchType { get; set; }
    }
}
