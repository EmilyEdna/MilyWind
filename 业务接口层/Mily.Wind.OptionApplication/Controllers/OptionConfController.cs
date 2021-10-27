using Microsoft.AspNetCore.Mvc;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionApplication.Controllers
{
    /// <summary>
    /// 配置服务
    /// </summary>
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = DSConst.Options)]
    public class OptionConfController : BasicController
    {
        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<OptionConfMogoViewModel> WriteOptionConf(OptionConfInput input) => OptionLogic.WriteOptionConf(input);
        /// <summary>
        /// 查询配置列
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Dictionary<string, List<OptionConfMogoViewModel>>> SearchOptionConf() => OptionLogic.SearchOptionConf();
        /// <summary>
        /// 修改配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<OptionConfMogoViewModel> AlterOptionConf(OptionConfInput input) => OptionLogic.AlterOptionConf(input);
        /// <summary>
        /// 获取配置记录
        /// </summary>
        /// <param name="CId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<OptionConfVerMogoViewModel>> SearchOptionConfVer(string CId) => OptionLogic.SearchOptionConfVer(CId);
        /// <summary>
        /// 删除配置记录并获取
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<List<OptionConfVerMogoViewModel>> RemoveAndSearchOptionConfVer(Guid Id, string CId) => OptionLogic.RemoveAndSearchOptionConfVer(Id, CId);
        /// <summary>
        /// 还原记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<bool> RestoreOptionConf(Guid Id) => OptionLogic.RestoreOptionConf(Id);
    }
}
