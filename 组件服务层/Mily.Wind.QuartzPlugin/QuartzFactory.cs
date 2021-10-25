using Microsoft.Extensions.DependencyInjection;
using Mily.Wind.QuartzPlugin.QuartzCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.QuartzPlugin
{
    public class QuartzFactory
    {
        private IServiceCollection services;
        private string assemblyName;

        public QuartzFactory(IServiceCollection services, string assemblyName)
        {
            this.services = services;
            this.assemblyName = assemblyName;
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

        public QuartzFactory StartAllSimpleJob(int minutes, string des = "")
        {
            IQuartzCorePlugin quartz = this.services.BuildServiceProvider().GetService<IQuartzCorePlugin>() ?? throw new NullReferenceException("please use QuartzExtension to initialize");
            if (assemblyName.IsNullOrEmpty()) throw new Exception("请输入指定的程序集名称");
            SyncStatic.Assembly(assemblyName).SelectMany(t => t.ExportedTypes.Where(x => x.BaseType == typeof(JobBasic))).ForEnumerEach(item =>
            {
                quartz.CreateAllSimpleJob(item, minutes, des);
            });
            return this;
        }

        public QuartzFactory StartAllCronJob(string cron, string des = "")
        {
            IQuartzCorePlugin quartz = this.services.BuildServiceProvider().GetService<IQuartzCorePlugin>() ?? throw new NullReferenceException("please use QuartzExtension to initialize");
            SyncStatic.Assembly(assemblyName).SelectMany(t => t.ExportedTypes.Where(x => x.BaseType == typeof(JobBasic))).ForEnumerEach(item =>
            {
                quartz.CreateAllCronJob(item, cron, des);
            });
            return this;
        }

        public IServiceCollection Complete() => this.services;
    }
}
