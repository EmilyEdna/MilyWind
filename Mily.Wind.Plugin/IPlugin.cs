using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Execute(object inPars);
    }
}
