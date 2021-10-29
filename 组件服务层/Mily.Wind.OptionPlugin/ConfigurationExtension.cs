using Microsoft.Extensions.Configuration;
using Mily.Wind.OptionPlugin.ConfigurationImpl;
using Mily.Wind.OptionPlugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionPlugin
{
    public static class ConfigurationExtension
    {

        public static IConfigurationBuilder UseConfiguration(this IConfigurationBuilder builder,Action<ConfigurationOption> action)
        {

           /* .ConfigureAppConfiguration((context, builder) =>
             {
                 builder.UseConfiguration(opt =>
                 {
                     opt.Env = EnvEnum.Dev;
                     opt.NameSpace = "Test";
                     opt.Url = "http://127.0.0.1:9998";
                 });
             })*/

            ConfigurationOption opt = new ConfigurationOption();
            action(opt);
            ConfigurationIoc.Set(opt);
            return builder.Add(new CustomConfigurationSource());
        }
    }
}
