using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyResult
    {
        public int HttpCode { get; set; }
        public bool Success => HttpCode == 200;
        public object Result { get; set; }
        public static MilyResult Instance(Action<MilyResult> action)
        {
            MilyResult result = new MilyResult();
            action(result);
            return result;
        }
    }
}