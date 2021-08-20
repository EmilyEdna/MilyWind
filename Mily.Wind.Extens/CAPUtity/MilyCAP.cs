using DotNetCore.CAP;
using Mily.Wind.Extens.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.CAPUtity
{
    public class MilyCAP : ICapSubscribe
    {
        public void Publisher<T>(string topic, T param)
        {
            ICapPublisher publisher = MilyUtily.GetService<ICapPublisher>();
            publisher.Publish(topic, param);
        }
        public async Task PublisherAsync<T>(string topic, T param)
        {
            ICapPublisher publisher = MilyUtily.GetService<ICapPublisher>();
            await publisher.PublishAsync(topic, param);
        }

    }

}
