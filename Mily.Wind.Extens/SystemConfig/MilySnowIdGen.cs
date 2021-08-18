using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilySnowIdGen
    {
        private static DateTime epoch = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static IdStructure structure = new IdStructure(45, 2, 16);
        private static IdGeneratorOptions options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));
        public static IdGenerator IdGen = new IdGenerator(3, options);
    }
}
