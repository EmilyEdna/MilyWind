using DotNetCore.CAP;
using DryIoc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.DependencyInjection
{
    public class IocManager
    {
        private static readonly ConcurrentDictionary<string, IContainer> current = new ConcurrentDictionary<string, IContainer>();
        public static ICapPublisher CapBus { get; set; }
        public static IConfiguration Configuration => GetService<IConfiguration>();
        public static T GetService<T>()
        {
            current.TryGetValue(nameof(IocManager), out IContainer container);
            return container.Resolve<T>();
        }
        internal static void SetContainer(IContainer container)
        {
            current.TryAdd(nameof(IocManager), container);
        }
    }
}
