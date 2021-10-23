using Microsoft.Extensions.DependencyInjection;
using Mily.Wind.QuartzPlugin.QuartzCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin
{
    public class QuartzFactory
    {
        private IServiceCollection services;

        public QuartzFactory(IServiceCollection services)
        {
            this.services = services;
        }

        public QuartzFactory StartSimpleJob<T>(int minutes, string des = "") where T : JobBasic
        {
            IQuartzCorePlugin quartz = this.services.BuildServiceProvider().GetService<IQuartzCorePlugin>() ?? throw new NullReferenceException("please use QuartzExtension to initialize");
            quartz.CreateSimpleJob<T>(minutes, des);
            return this;
        }

        public QuartzFactory StartCronJob<T>(string cron, string des = "") where T : JobBasic
        {
            IQuartzCorePlugin quartz = this.services.BuildServiceProvider().GetService<IQuartzCorePlugin>() ?? throw new NullReferenceException("please use QuartzExtension to initialize");
            quartz.CreateCronJob<T>(cron, des);
            return this;
        }

        public IServiceCollection Complete() => this.services;
    }
}
