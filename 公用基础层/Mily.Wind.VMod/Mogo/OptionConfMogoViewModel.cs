using Mily.Wind.VMod.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo
{
    public class OptionConfMogoViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public string OptionJson { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 环境
        /// </summary>
        public EnvEnum Env { get; set; }
    }
}
