using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Mily.Wind.Logic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mily.Wind.HostApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult<object> Get()
        {
            IMainLogic logic = new MainLogic();
            return logic.GetUserList();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<object> Create()
        {
            IMainLogic logic = new MainLogic();
            var user = logic.CreateUser();
            return user;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<object> Login(long id)
        {
            IMainLogic logic = new MainLogic();
            var user = logic.GetUser(id);

            //从数据库验证用户名，密码
            //验证通过 否则 返回Unauthorized

            //创建claim
            var authClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This MilyWind is the latest micro service project ."));
            var token = new JwtSecurityToken(
                   issuer: "MilyWind",
                   audience: "MilyWind",
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );

            //返回token和过期时间
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
    }
}
