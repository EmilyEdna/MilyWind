using DotNetCore.CAP;
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
        public  void Publisher<T>(string topic, T param)
        {
            ICapPublisher publisher = MilyUtily.GetServiceByMsIoc<ICapPublisher>();
            publisher.Publish(topic, param);
        }
        public  async Task PublisherAsync<T>(string topic, T param)
        {
            ICapPublisher publisher = MilyUtily.GetServiceByMsIoc<ICapPublisher>();
            await publisher.PublishAsync(topic, param);
        }

    }

    public class CapSubscribe : ICapSubscribe
    {
        [CapSubscribe("Test")]
        public void Subscribe(JToken obj)
        {
            var xx = obj;
        }
    }

}
