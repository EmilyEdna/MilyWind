using Mily.Wind.VMod.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.InternalInterface
{
    public interface ILog
    {
        void WriteLog(string Message, string Invoken, List<object> Param = null, LogLevelEnum Lv = LogLevelEnum.Info);
        Task WriteLogAsync(string Message, string Invoken, List<object> Param = null, LogLevelEnum Lv = LogLevelEnum.Info);
    }
}
