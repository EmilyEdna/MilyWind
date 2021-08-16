using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyMiddleWare
    {
        private readonly RequestDelegate _request;
        public MilyMiddleWare(RequestDelegate RequestDelegates)
        {
            _request = RequestDelegates;
        }

        public async Task Invoke(HttpContext Context) 
        {
            await _request(Context);
        }
    }
}
