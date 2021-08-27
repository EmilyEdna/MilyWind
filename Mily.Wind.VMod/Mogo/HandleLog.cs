using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo
{
    public class HandleLog
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public long? UserId { get; set; }
        public int? TenantId { get; set; }
        public string TargetTable { get; set; }
        public string HandleLogs { get; set; }
        public DateTime? HandleTime { get; set; }
    }
}
