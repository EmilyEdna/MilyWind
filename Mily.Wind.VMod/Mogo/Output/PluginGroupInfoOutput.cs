using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Output
{
    public class PluginGroupInfoOutput : IVMCastle
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public List<PluginGroupKVOutput> GroupValue { get; set; }
        public string DSCode { get; set; }
    }
    public class PluginGroupKVOutput
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
