using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
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

        Dictionary<string, List<OptionConfMogoViewModel>> SearchOptionConf();

        OptionConfMogoViewModel AlterOptionConf(OptionConfInput input);

        List<OptionConfVerMogoViewModel> SearchOptionConfVer(string CId);

        List<OptionConfVerMogoViewModel> RemoveAndSearchOptionConfVer(Guid Id, string CId);

        bool RestoreOptionConf(Guid Id);

    }
}
