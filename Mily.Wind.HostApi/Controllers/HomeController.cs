using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Mily.Wind.Extens.SystemConfig;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mily.Wind.HostApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : BasicController
    {
        [HttpGet]
        [Authorize]
        public ActionResult<object> Get()
        {
            return MainLogic.GetUserList();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<object> Create()
        {
            return MainLogic.CreateUser();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<object> Login(long id)
        {
            var user = MainLogic.GetUser(id);
            var token = MilyJwtSecurity.JwtToken(new string[] { user.Name, user.Id.ToString() });
            //返回token和过期时间
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
    }
}
