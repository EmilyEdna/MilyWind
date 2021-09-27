using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo
{
    public class PluginMapperInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 插件原始名称
        /// </summary>
        public string PluginName { get; set; }
        /// <summary>
        /// 插件别名
        /// </summary>
        public string PluginAlias { get; set; }
        /// <summary>
        /// 插件大小
        /// </summary>
        public string PluginSize { get; set; }
        /// <summary>
        /// 组件路径
        /// </summary>
        public string PluginPath { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEable { get; set; } 
        /// <summary>
        /// 插件版本号
        /// </summary>
        public int PluginVersion { get; set; } 
        /// <summary>
        /// 插件注册时间
        /// </summary>
        public DateTime? RegistTime { get; set; } 
    }
}
