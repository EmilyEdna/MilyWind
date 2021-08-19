using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mily.Wind.HostApi
{
    public class BasicController: Controller
    {
        protected IMainLogic MainLogic = MilyDryIoc.GetService<IMainLogic>();
    }
}
