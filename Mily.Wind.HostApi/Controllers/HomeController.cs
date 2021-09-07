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
        public ActionResult<List<MilyUserVM>> Get()
        {
            return MainLogic.GetUserList().Result.Transfers<MilyUserVM>();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public ActionResult<List<MilyUserVM>> Create()
        {
            return MainLogic.CreateUser().Result.Transfers<MilyUserVM>();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ActionResult<object> Login(long id)
        {

            var user = MainLogic.GetUser(id).Result.Transfer<MilyUserVM>();
            var token = MilyJwtSecurity.JwtToken(new string[] { user?.Name, user?.Id.ToString() });
            //返回token和过期时间
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
    }
}
