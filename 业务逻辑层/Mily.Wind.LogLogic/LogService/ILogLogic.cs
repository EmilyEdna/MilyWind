using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogLogic.LogService
{
    public interface ILogLogic: ILogic
    {
        LogOutput GetLogPage(LogInput input);
        bool DeleteLog(Guid id);
        List<string> GetSystemService();
        bool WriteLog(List<LogWriteInput> input);
    }
}
