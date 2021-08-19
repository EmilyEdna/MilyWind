using DryIoc;
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
    public class MilyDryIoc
    {
        private static readonly ConcurrentDictionary<string, IContainer> current = new ConcurrentDictionary<string, IContainer>();

        public static T GetService<T>()
        {
            current.TryGetValue(nameof(MilyDryIoc), out IContainer container);
            return container.Resolve<T>();
        }
        internal static void SetContainer(IContainer container)
        {
            current.TryAdd(nameof(MilyDryIoc), container);
        }
    }
}
