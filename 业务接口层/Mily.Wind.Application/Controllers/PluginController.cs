using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mily.Wind.Application.Controllers
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
        [HttpPost, AllowAnonymous, Consumes("application/json", "multipart/form-data")]
        public ActionResult<MilyCtrlResult<object>> UploadPlugin(List<IFormFile> files)
        {
            List<object> list = new List<object>();
            files.ForEach(file =>
            {
                if (Regex.IsMatch(file.FileName, "(.*?).(dll|zip)"))
                    list.Add(new { FileName = file.FileName, Success = true });
                else
                    list.Add(new { FileName = file.FileName, Success = false });
            });
            var res = PluginLogic.UploadPlugin(files);
            return MilyCtrlResult<object>.CreateResult(t =>
            {
                t.Code = res.Code;
                t.Result = list;
            });
        }
        /// <summary>
        /// 插件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult<MilyCtrlResult<PluginOutput>> GetPluginPage(PluginInput input)
        {
            var data = PluginLogic.GetPluginPage(input).Result.Transfer<PluginOutput>();
            return MilyCtrlResult<PluginOutput>.CreateResult(t =>
            {
                t.Code = data.DSCode;
                t.Result = data;
            });
        }
        /// <summary>
        /// 更新插件的别名
        /// </summary>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public ActionResult<MilyCtrlResult<bool>> AlterPlugin(PluginAlterInput input)
        {
            var data = PluginLogic.AlterPlugin(input);
            return MilyCtrlResult<bool>.CreateResult(t =>
            {
                t.Code = data.Code;
                t.Result = (bool)data.Result;
            });
        }

    }
}
