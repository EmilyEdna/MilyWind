using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mily.Wind.OptionPlugin;
using Mily.Wind.OptionPlugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mily.Wind.LogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, builder) =>
            {
                builder.UseConfiguration(opt =>
                {
                    opt.Env = EnvEnum.Dev;
                    opt.NameSpace = "Log";
                    opt.Url = "http://127.0.0.1:9998";
                });
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
