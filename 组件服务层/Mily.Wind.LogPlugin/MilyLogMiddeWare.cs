using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin
{
    public class MilyLogMiddeWare
    {
        private readonly RequestDelegate _request;
        public MilyLogMiddeWare(RequestDelegate RequestDelegates)
        {
            _request = RequestDelegates;
        }

        public async Task Invoke(HttpContext Context)
        {
            await _request(Context);
        }
    }
}
