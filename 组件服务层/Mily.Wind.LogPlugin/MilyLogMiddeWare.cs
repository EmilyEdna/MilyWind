using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;

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
            MilyLogOption.TraceNode = $"{DateTime.Now.ToFmtDate(3,"yyyyMMdd")}.{DateTime.Now.Ticks}.{Guid.NewGuid().ToString().Replace("-", "")}";
            await _request(Context);
        }
    }
}
