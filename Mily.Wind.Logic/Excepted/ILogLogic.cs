using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo.Input;
using System;

namespace Mily.Wind.Logic.Excepted
{
    public interface ILogLogic: ILogic
    {
        MilyMapperResult GetLogPage(LogInput input);
        MilyMapperResult DeleteLog(Guid id);
    }
}
