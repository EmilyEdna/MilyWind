using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var NoAuthor = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(t => t.GetType() == typeof(AllowAnonymousAttribute));
            if (NoAuthor == null)
            {
                var securityToken = context.HttpContext.GetTokenAsync("Bearer", "access_token").Result;
                if (securityToken.IsNullOrEmpty())
                {
                    context.Result = new UnauthorizedObjectResult("401");
                    return;
                }
                JwtSecurityToken Token = new JwtSecurityToken(securityToken);
                var Name = Token.Claims.FirstOrDefault(t => t.Type == "sub").Value;
                var Id = Token.Claims.FirstOrDefault(t => t.Type == "nameid").Value;
                MilySession.Key = $"{Id}{Name}";
            }
        }
    }
}
