using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo
{
    public class HandleLog
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        public long? UserId { get; set; }
        public int? TenantId { get; set; }
        public string TargetTable { get; set; }
        public string HandleLogs { get; set; }
        public DateTime? HandleTime { get; set; }
    }
}
