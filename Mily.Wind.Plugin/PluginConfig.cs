using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mily.Wind.Plugin
{
    public class PluginConfig
    {
        public static string PluginRoute = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugin");
    }
}
