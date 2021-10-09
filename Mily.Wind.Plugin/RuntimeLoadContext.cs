using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin
{
    public class RuntimeLoadContext : AssemblyLoadContext
    {
        public RuntimeLoadContext() : base(true)
        {

        }
    }
}
