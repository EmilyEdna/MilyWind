using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin
{
    internal class LogIoc
    {
        private static ConcurrentDictionary<string, object> Ioc = new ConcurrentDictionary<string, object>();
        internal static void Set(string key, object value) 
        {
            if (!Ioc.ContainsKey(key))
                Ioc.TryAdd(key, value);
        }

        internal static T Get<T>(string key)
        {
            if (Ioc.ContainsKey(key))
            {
                Ioc.TryGetValue(key, out object value);
                return (T)value;
            }
            else return default;
        }
    }
}
