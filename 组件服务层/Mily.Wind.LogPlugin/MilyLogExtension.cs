using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.LogPlugin
{
    public static class MilyLogExtension
    {
        public static IApplicationBuilder UseMilyLog(this IApplicationBuilder app, Action<MilyLogOption> action)
        {
            MilyLogOption opt = new MilyLogOption();
            action(opt);
            if (opt.SystemService.IsNullOrEmpty())
                throw new ArgumentNullException("field 'SystemService' can't be null");
            if (opt.Scheme.IsNullOrEmpty())
                throw new ArgumentNullException("field 'Scheme' can't be null");
            if (opt.Host.IsNullOrEmpty())
                throw new ArgumentNullException("field 'Host' can't be null");
            LogIoc.Set(nameof(MilyLogOption), opt);
            app.UseMiddleware<MilyLogMiddeWare>();
            return app;
        }

    }
}
