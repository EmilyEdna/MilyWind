using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Logic.Main
{
    public interface IMainLogic: ILogic
    {
        MilyResult GetUserList();
        MilyResult GetUser(long id);
        MilyResult CreateUser();
    }
}
