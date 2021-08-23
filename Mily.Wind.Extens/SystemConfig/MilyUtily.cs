using DryIoc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyUtily
    {
        private static readonly ConcurrentDictionary<string, IContainer> current = new ConcurrentDictionary<string, IContainer>();
        public static IConfiguration Configuration => GetServiceByContainer<IConfiguration>();
        public static T GetServiceByContainer<T>()
        {
            current.TryGetValue(nameof(MilyUtily), out IContainer container);
            return container.Resolve<T>();
        }
        public static T GetServiceByMsIoc<T>()
        {
            current.TryGetValue(nameof(MilyUtily), out IContainer container);
           return container.GetService<T>();
        }
        internal static void SetContainer(IContainer container)
        {
            current.TryAdd(nameof(MilyUtily), container);
        }
    }
}
