using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.LogApplication;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;

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
        [HttpPost]
        public ActionResult<LogOutput> GetLogPage(LogInput input) => LogLogic.GetLogPage(input);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<bool> DeleteLog(Guid Id) => LogLogic.DeleteLog(Id);

        /// <summary>
        /// 获取服务类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<string>> GetSystemService() => LogLogic.GetSystemService();

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> WriteLog(List<LogWriteInput> input) => LogLogic.WriteLog(input);
    }
}
