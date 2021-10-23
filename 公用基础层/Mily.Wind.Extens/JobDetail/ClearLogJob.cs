using Mily.Wind.QuartzPlugin;
using Mily.Wind.VMod.Mogo;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;

namespace Mily.Wind.Extens.JobDetail
{
    public class ClearLogJob:JobBasic
    {
        public override Task Execute(IJobExecutionContext context)
        {
            return base.Execute(context);
        }
    }
}
