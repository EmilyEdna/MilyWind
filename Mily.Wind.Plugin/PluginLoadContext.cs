using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

namespace Mily.Wind.Plugin
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _Resolver;
        public PluginLoadContext(string dllPath) : base(true)
        {
            _Resolver = new AssemblyDependencyResolver(dllPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string AssemblyPath = _Resolver.ResolveAssemblyToPath(assemblyName);
            if (AssemblyPath != null)
                return LoadFromAssemblyPath(AssemblyPath);
            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string DependencyPath = _Resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (DependencyPath != null)
                return LoadUnmanagedDllFromPath(DependencyPath);
            return IntPtr.Zero;
        }
    }
}
