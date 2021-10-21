using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Logic.Main;
using Mily.Wind.Logic.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Application
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
        protected IPluginLogic PluginLogic = IocManager.GetService<IPluginLogic>();
    }
}
