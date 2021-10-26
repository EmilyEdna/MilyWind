using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Output
{
    public class PluginOutput : IVMCastle
    {
        public int Total { get; set; }
        public List<PluginMogoViewModel> Detail { get; set; }
        public string DSCode { get; set; }
    }
}
