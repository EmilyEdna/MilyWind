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
        MilyMapperResult GetUserList();
        MilyMapperResult GetUser(long id);
        MilyMapperResult CreateUser();
    }
}
