using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin.Infos
{
    public class PluginMethodInfo
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        /// <summary>
        /// 插件Id
        /// </summary>
        public string PluginId { get; set; }
        /// <summary>
        /// 插件类Id
        /// </summary>
        public string PluginClassId { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDescription { get; set; }
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
