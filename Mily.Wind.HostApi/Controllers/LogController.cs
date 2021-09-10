using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
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
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = DSConst.Logs)]
    public class LogController : BasicController
    {
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult<MilyCtrlResult<LogOutput>> GetLogPage(LogInput input)
        {
            var data = LogLogic.GetLogPage(input).Result.Transfer<LogOutput>();
            return MilyCtrlResult<LogOutput>.CreateResult(t =>
            {
                t.Code = data.DSCode;
                t.Result = data;
            });
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete,AllowAnonymous]
        public ActionResult<MilyCtrlResult<bool>> DeleteLog(Guid Id) 
        {
            var res = LogLogic.DeleteLog(Id);
            return MilyCtrlResult<bool>.CreateResult(t =>
            {
                t.Code = res.Code;
                t.Result = (bool)res.Result;
            });
        }
    }
}
