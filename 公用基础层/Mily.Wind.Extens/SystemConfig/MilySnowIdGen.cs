using IdGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using XExten.Advance.CacheFramework;

namespace Mily.Wind.Extens.SystemConfig
{
    public static class MilySnowIdGen
    {

        private static readonly DateTime epoch = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly IdStructure structure = new IdStructure(41, 10, 12);
        private static readonly IdGeneratorOptions options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));
        public static readonly IdGenerator IdGen = new IdGenerator(Seed, options);
        private static int Seed = 0;
        static MilySnowIdGen()
        {
            Seed = new Random(Guid.NewGuid().GetHashCode()).Next(0, 1024);
            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(t => t.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
            var dic = Caches.RedisCacheGet<Dictionary<string, int?>>(nameof(MilySnowIdGen));
            if (dic == null)
            {
                dic = new Dictionary<string, int?> { { ip, Seed } };
                Caches.RedisCacheSet(nameof(MilySnowIdGen), dic, 14400);//10day
            }
            else
            {
                Seed = CreateSeed(dic, Seed,ip);
                if (dic[ip] == null)
                {
                    dic[ip] = Seed;
                }
                Caches.RedisCacheSet(nameof(MilySnowIdGen), dic, 14400);
            }
        }

        private static int CreateSeed(Dictionary<string, int?> pairs,int seed,string ip) 
        {
            if (pairs.Values.Contains(Seed)&&!pairs.ContainsKey(ip))
            {
                seed = new Random(Guid.NewGuid().GetHashCode()).Next(0, 1024);
                 return  CreateSeed(pairs, seed,ip);
            }
            return seed;
        }
    }
}
