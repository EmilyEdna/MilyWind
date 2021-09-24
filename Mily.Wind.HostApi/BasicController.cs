using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Logic.Excepted;
using Mily.Wind.Logic.Main;
using Mily.Wind.Logic.Plugin;

namespace Mily.Wind.HostApi
{
    public class BasicController : Controller
    {
        private ICapPublisher _CapBus;
        [PropertyInjection]
        protected ICapPublisher CapBus
        {
            get { return _CapBus; }
            set
            {
                IocManager.CapBus = value;
                _CapBus = value;
            }
        }
        protected IMainLogic MainLogic = IocManager.GetService<IMainLogic>();
        protected ILogLogic LogLogic = IocManager.GetService<ILogLogic>();
        protected IPluginLogic PluginLogic = IocManager.GetService<IPluginLogic>();
    }
}
