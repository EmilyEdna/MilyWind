using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.QuartzPlugin.QuartzCore
{
    public interface IQuartzCorePlugin
    {
        Task CreateSimpleJob<T>(int minutes, string des = "") where T : JobBasic;
        Task CreateCronJob<T>(string cron, string des = "") where T : JobBasic;
    }
}
