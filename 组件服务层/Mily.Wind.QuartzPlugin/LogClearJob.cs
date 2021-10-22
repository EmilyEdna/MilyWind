using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin
{
    public class LogClearJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("开始执行日志清理任务");
        }
    }
}
