using DotNetCore.CAP;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.SystemConfig;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.CAPUtity
{
    public class MilyCAP
    {
        public static void Publisher<T>(string topic, T param)
        {
            IocManager.CapBus.Publish(topic, param);
        }
        public static async Task PublisherAsync<T>(string topic, T param)
        {
            await IocManager.CapBus.PublishAsync(topic, param);
        }

    }
}
