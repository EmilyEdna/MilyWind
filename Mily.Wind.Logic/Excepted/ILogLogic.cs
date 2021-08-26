using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Logic.Excepted
{
    public interface ILogLogic: ILogic
    {
        LogOutput GetLogPage(LogInput input);
    }
}
