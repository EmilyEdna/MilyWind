using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.LogLogic.LogService;

namespace Mily.Wind.LogApplication
{
    public class BasicController : Controller
    {
        protected ILogLogic LogLogic = IocManager.GetService<ILogLogic>();
    }
}
