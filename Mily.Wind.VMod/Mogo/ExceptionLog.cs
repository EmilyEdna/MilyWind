using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo
{
    public class ExceptionLog
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string Invoken { get; set; }
        public string Trace { get; set; }
        public DateTime CreatedTime { get; set; }
        public string EntityName { get; set; }
        public List<object> Param { get; set; } 
        public string ErrorMsg { get; set; }
    }
}
