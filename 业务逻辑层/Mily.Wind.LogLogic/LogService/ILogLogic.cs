using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogLogic.LogService
{
    public interface ILogLogic: ILogic
    {
        MilyMapperResult GetLogPage(LogInput input);
        MilyMapperResult DeleteLog(Guid id);
        MilyMapperResult GetSystemService();
        MilyMapperResult WriteLog(List<LogWriteInput> input);
    }
}
