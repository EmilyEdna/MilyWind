using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.SugarEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Logic
{
    public interface IMainLogic: ILogic
    {
        List<MilyUser> GetUserList();
        MilyUser GetUser(long id);
        MilyUser CreateUser();
    }
}
