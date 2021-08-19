using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyJwtSecurity
    {
        public static JwtSecurityToken JwtToken(string[] arg)
        {
            //创建claim
            var authClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,arg[0]),
                new Claim(JwtRegisteredClaimNames.NameId,arg[1]),
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
            return token;
        }
    }
}
