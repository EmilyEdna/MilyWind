using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

namespace Mily.Wind.Plugin
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext() : base(true)
        {
           
        }
    }
}
