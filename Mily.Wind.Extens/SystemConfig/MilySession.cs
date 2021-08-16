using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilySession
    {
        internal static string Key { get; set; }
        public static T GetSession<T>()
        {
            if (Key.IsNullOrEmpty()) return default;
            return Caches.RedisCacheGet<T>(Key);
        }
        public static T GetSession<T>(string Key)
        {
            if (Key.IsNullOrEmpty()) return default;
            return Caches.RedisCacheGet<T>(Key);
        }
    }
}
