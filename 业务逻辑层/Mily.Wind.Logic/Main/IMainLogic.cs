using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.DataTransferObj.Input;

namespace Mily.Wind.Logic.Main
{
    public interface IMainLogic: ILogic
    {
        MilyMapperResult GetUserList();
        MilyMapperResult GetUser(long id);
        MilyMapperResult CreateUser(MilyUserVMInput input);
    }
}
