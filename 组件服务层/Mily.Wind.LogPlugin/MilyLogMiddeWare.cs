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
            MilyLogOption.TraceNode = $"{DateTime.Now.Ticks}.{Context.Request.Path.ToString()}.{Guid.NewGuid().ToString().Replace("-", "")}";
            await _request(Context);
        }
    }
}
