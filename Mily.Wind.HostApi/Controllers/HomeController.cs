using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity.System;
using Mily.Wind.VMod.DataTransferObj;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.HostApi.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController, Route("[controller]/[action]"), ApiExplorerSettings(GroupName = "v1")]
    public class HomeController : BasicController
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<MilyCtrlResult<List<MilyUserVM>>> Get()
        {
            var data = MainLogic.GetUserList();
            return MilyCtrlResult<List<MilyUserVM>>.CreateResult(t =>
             {
                 t.Code = data.Code;
                 t.Result = data.Result.Transfers<MilyUserVM>();
             });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public ActionResult<MilyCtrlResult<MilyUserVM>> Create(MilyUserVM input)
        {
            var data = MainLogic.CreateUser(input);
            return MilyCtrlResult<MilyUserVM>.CreateResult(t =>
            {
                t.Code = data.Code;
                t.Result = data.Result.Transfer<MilyUserVM>();
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
            var user = data.Result.Transfer<MilyUserVM>();
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
