using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin
{
    public interface IPluginLoad
    {
        void SetDirectory(string directory);
        void RegistPlugin(Dictionary<string, string> dllInfo);
        object Excute(string module, string excuteMethod, params object[] param);
    }
}
