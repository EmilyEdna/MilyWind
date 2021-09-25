using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class PluginAlterInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string PluginAlias { get; set; }
        /// <summary>
        /// 操作类型 1启用 0禁用 -1删除
        /// </summary>
        public int? Type { get; set; }
    }
}
