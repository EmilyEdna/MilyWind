using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;

namespace Mily.Wind.HostApi.Controllers
{
    /// <summary>
    /// 插件系统
    /// </summary>
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = DSConst.Plugins)]
    public class PluginController : BasicController
    {
        /// <summary>
        /// 上传插件
        /// </summary>
        /// <returns></returns>
        [HttpPost,AllowAnonymous,Consumes("application/json", "multipart/form-data")]
        public ActionResult<MilyCtrlResult<bool>> UploadPlugin(List<IFormFile> files)
        {
            var x = Request;
            return MilyCtrlResult<bool>.CreateResult(t =>
            {
                t.Code = DSConst.DS001;
                t.Result = true; 
            });
        }
    }
}
