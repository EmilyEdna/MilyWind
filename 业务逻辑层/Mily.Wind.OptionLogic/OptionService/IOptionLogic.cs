using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionLogic.OptionService
{
    public interface IOptionLogic : ILogic
    {
        OptionConfMogoViewModel WriteOptionConf(OptionConfInput input);
        OptionConfMogoViewModel GetOptionConfFirst(Guid Id);
        Dictionary<string, List<OptionConfMogoViewModel>> SearchOptionConf();
        OptionConfMogoViewModel AlterOptionConf(OptionConfInput input);
        OptionConfVerPageOutput SearchOptionConfVer(OptionConfVerPageInput input);
        bool RemoveAndSearchOptionConfVer(Guid Id);
        bool RestoreOptionConf(Guid Id);
         int GetChange(OptionConfInput input);
         object GetOptionConf(OptionConfInput input);
    }
}
