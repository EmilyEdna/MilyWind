using Microsoft.Extensions.DependencyInjection;
using Mily.Wind.QuartzPlugin.QuartzCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin
{
    public static class QuartzExtension
    {
        public static QuartzFactory AddQuartz(this IServiceCollection services) 
        {
            services.AddSingleton<IQuartzCorePlugin, QuartzCorePlugin>();
            return new QuartzFactory(services);
        }
    }
}
