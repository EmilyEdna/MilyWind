using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin
{
    public class QuartzPlugin
    {
        private Task<IScheduler> instance = new StdSchedulerFactory().GetScheduler();

        public Task CreateJob<T>(int hours) where T : IJob
        {
            instance.Result.Start();

            var jobs = JobBuilder.Create<T>().Build();

            var trigger = TriggerBuilder.Create().WithSimpleSchedule(t => t.WithIntervalInHours(hours).RepeatForever())
                  .WithDescription("日志清理任务").Build();

            return instance.Result.ScheduleJob(jobs, trigger);
        }
    }
}
