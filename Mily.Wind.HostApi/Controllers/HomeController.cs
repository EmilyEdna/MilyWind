using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mily.Wind.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mily.Wind.HostApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            IMainLogic logic = new MainLogic();
            logic.Test();
            return new JsonResult(new { Name = "Ok" });
        }
    }
}
