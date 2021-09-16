using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
using Mily.Wind.VMod.DataTransferObj.Input;
using Mily.Wind.VMod.DataTransferObj.Output;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Mily.Wind.HostApi.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = DSConst.Logics)]
    public class HomeController : BasicController
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ActionResult<MilyCtrlResult<List<MilyUserVMOutput>>> Get()
        {
            var data = MainLogic.GetUserList();
            return MilyCtrlResult<List<MilyUserVMOutput>>.CreateResult(t =>
             {
                 t.Code = data.Code;
                 t.Result = data.Result.Transfers<MilyUserVMOutput>();
             });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public ActionResult<MilyCtrlResult<MilyUserVMOutput>> Create(MilyUserVMInput input)
        {
            var data = MainLogic.CreateUser(input);
            return MilyCtrlResult<MilyUserVMOutput>.CreateResult(t =>
            {
                t.Code = data.Code;
                t.Result = data.Result.Transfer<MilyUserVMOutput>();
            });
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ActionResult<MilyCtrlResult<object>> Login(long id)
        {
            var data = MainLogic.GetUser(id);
            var user = data.Result.Transfer<MilyUserVMOutput>();
            var token = MilyJwtSecurity.JwtToken(new string[] { user?.Name, user?.Id.ToString() });
            //返回token和过期时间
            return MilyCtrlResult<object>.CreateResult(t =>
            {
                t.Code = data.Code;
                t.Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            });
        }
    }
}
