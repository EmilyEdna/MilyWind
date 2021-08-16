using Mily.Wind.Extens.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.SugarEntity
{
    public class MongoEntity
    {
        public int? UserId { get; set; }
        public int? TenantId { get; set; }
        public string TargetTable { get; set; }
        public string HandleLog { get; set; }
        public DateTime? HandleTime { get; set; }
    }
}
