using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin.Infos
{
    public class PluginGroupExcuteInfo
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        /// <summary>
        /// 插件Id
        /// </summary>
        public string PluginId { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 执行方法的简写键
        /// </summary>
        public string ExcuteKey { get; set; }
        /// <summary>
        /// 执行方法的完整值
        /// </summary>
        public string ExcuteValue { get; set; }
    }
}
