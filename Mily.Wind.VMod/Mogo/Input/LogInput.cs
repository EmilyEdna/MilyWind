using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class LogInput
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 100;
    }
}
