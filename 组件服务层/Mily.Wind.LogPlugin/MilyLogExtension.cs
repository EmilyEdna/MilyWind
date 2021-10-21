using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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
        /// <summary>
        /// 启用分布式日志
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMilyLog(this IApplicationBuilder app, Action<MilyLogOption> action)
        {
            MilyLogOption opt = new MilyLogOption();
            action(opt);
            if (opt.SystemService.IsNullOrEmpty())
                throw new ArgumentNullException("field 'SystemService' can't be null");
            if (opt.Url.IsNullOrEmpty())
                throw new ArgumentNullException("field 'Url' can't be null");
            LogIoc.Set(nameof(MilyLogOption), opt);
            app.UseMiddleware<MilyLogMiddeWare>();
            return app;
        }

        /// <summary>
        /// 注册分布式日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMilyLog(this IServiceCollection services) 
        {
            services.AddSingleton<ILog, Log>();
            return services;
        }
    }
}
