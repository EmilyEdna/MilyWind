using IdGen;
using System;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilySnowIdGen
    {
        public static long CreateGenId()
        {
            DateTime epoch = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            IdStructure structure = new IdStructure(41, 10, 12);
            IdGeneratorOptions options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));
            int Seed = new Random(Guid.NewGuid().GetHashCode()).Next(0, 1024);
            IdGenerator IdGen = new IdGenerator(Seed, options);
            long GenId = IdGen.CreateId();
            return GenId;
        }
    }
}
