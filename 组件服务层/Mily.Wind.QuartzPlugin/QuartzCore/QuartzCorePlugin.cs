using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin.QuartzCore
{
    public class QuartzCorePlugin : IQuartzCorePlugin
    {
        private IScheduler instance = new StdSchedulerFactory().GetScheduler().Result;

        public Task CreateAllCronJob(Type type, string cron, string des = "")
        {
            Console.WriteLine("CronJob Is Created");
            instance.Start();
            var job = JobBuilder.Create(type).Build();
            var trigger = TriggerBuilder.Create().WithCronSchedule(cron)
                .WithDescription(des).Build();
            return instance.ScheduleJob(job, trigger);
        }

        public Task CreateAllSimpleJob(Type type, int minutes, string des = "")
        {
            Console.WriteLine("SimpleJob Is Created");
            instance.Start();
            var jobs = JobBuilder.Create(type).Build();
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(t => t.WithIntervalInMinutes(minutes).RepeatForever())
                .WithDescription(des).Build();
            return instance.ScheduleJob(jobs, trigger);
        }

        public Task CreateCronJob<T>(string cron, string des = "") where T : JobBasic
        {
            Console.WriteLine("CronJob Is Created");
            instance.Start();
            var job = JobBuilder.Create<T>().Build();
            var trigger = TriggerBuilder.Create().WithCronSchedule(cron)
                .WithDescription(des).Build();
            return instance.ScheduleJob(job, trigger);
        }

        public Task CreateSimpleJob<T>(int minutes, string des = "") where T : JobBasic
        {
            Console.WriteLine("SimpleJob Is Created");
            instance.Start();
            var jobs = JobBuilder.Create<T>().Build();
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(t => t.WithIntervalInMinutes(minutes).RepeatForever())
                .WithDescription(des).Build();
            return instance.ScheduleJob(jobs, trigger);
        }
    }
}
