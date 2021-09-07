using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mily.Wind.HostApi.Controllers
{
    /// <summary>
    /// 日志系统
    /// </summary>
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = "v2")]
    public class LogController : BasicController
    {
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ActionResult<LogOutput> GetLogPage(LogInput input)
        {
            return LogLogic.GetLogPage(input).Result.Transfer<LogOutput>();
        }


    }
}
