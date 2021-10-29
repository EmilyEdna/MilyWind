using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionPlugin.Settings
{
    public class ConfigurationOption
    {
        public string Url { get; set; }
        public EnvEnum Env { get; set; }
        public int Interval { get; set; } = 30;
        public string NameSpace { get; set; }
    }
}
