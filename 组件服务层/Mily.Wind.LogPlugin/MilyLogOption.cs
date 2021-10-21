using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin
{
    public class MilyLogOption
    {
        internal const string Route = "/Log/WriteLog";
        public string Scheme { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string SystemService { get; set; }
        public bool IsDevelopment { get; set; }
    }
}
