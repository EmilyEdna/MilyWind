using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin
{
    public class JobBasic : IJob
    {
        public virtual Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
