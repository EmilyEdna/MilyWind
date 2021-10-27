using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.OptionLogic.OptionService;

namespace Mily.Wind.OptionApplication
{
    public class BasicController : Controller
    {
        protected IOptionLogic OptionLogic = IocManager.GetService<IOptionLogic>();
    }
}
