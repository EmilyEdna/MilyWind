using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.SugarEntity.Mogo
{
    public class HandleLog
    {
        public long? UserId { get; set; }
        public int? TenantId { get; set; }
        public string TargetTable { get; set; }
        public string HandleLogs { get; set; }
        public DateTime? HandleTime { get; set; }
    }
}
