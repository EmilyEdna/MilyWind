using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionPlugin
{
    internal class ConfigurationIoc
    {
        private static Dictionary<string, object> Ioc = new Dictionary<string, object>();
        internal static void Set(object services)
        {
            if (!Ioc.ContainsKey(services.GetType().Name))
                Ioc.Add(services.GetType().Name, services);
        }

        internal static void Set(string key, object services)
        {
            if (!Ioc.ContainsKey(key))
                Ioc.Add(key, services);
        }

        internal static T GetService<T>(string key)
        {
            if (Ioc.ContainsKey(key))
                return (T)Ioc[key];
            else return default;
        }
    }
}
