using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo.Input;

namespace Mily.Wind.Logic.Excepted
{
    public interface ILogLogic: ILogic
    {
        MilyResult GetLogPage(LogInput input);
    }
}
