using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using XExten.Advance.CacheFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Extens.SystemConfig.SystemExten
{
    public static class MilyLogServiceCollection
    {
        private static IConfiguration Configuration { get; set; }
        public static IConfiguration RegisterLogConfiguration(this IConfiguration configuration)
        {
            Caches.DbName = configuration.GetConnectionString("MongoName");
            Caches.MongoDBConnectionString = configuration.GetConnectionString("Mongo");
            Configuration = configuration;
            return configuration;
        }

        public static IServiceCollection RegistLog(this IServiceCollection services)
        {
            RegistApi(services);

            RegistIoc(services);

            return services;
        }

        public static IServiceCollection RegistApi(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
                opt.SuppressInferBindingSourcesForParameters = true;
                opt.SuppressConsumesConstraintForFormFileParameters = true;
            });

            services.AddControllers(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
                opt.Conventions.Add(new MilyModelBinding());
            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                opt.SerializerSettings.Converters.Add(new MilyJsonConvert());
            }).AddControllersAsServices();

            return services;
        }

        public static IServiceCollection RegistIoc(this IServiceCollection services)
        {
            var AllAssemblies = SyncStatic.Assembly("Mily.Wind");
            var LogicServices = AllAssemblies.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ILogic)))).ToList();
            //var LogServices = AllAssemblies.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ILog)))).ToList();
            //日志
            //LogServices.ForEach(item =>
            //{
            //    if (item.IsClass)
            //    {
            //        var interfaces = item.GetInterfaces().Where(imp => imp == typeof(ILog)).FirstOrDefault();
            //        services.AddSingleton(interfaces, item);
            //    }
            //});
            //服务
            LogicServices.ForEach(item =>
            {
                if (item.IsClass)
                {
                    var interfaces = item.GetInterfaces().Where(imp => imp.GetInterfaces().Contains(typeof(ILogic))).FirstOrDefault();
                    services.AddSingleton(interfaces,  item);
                }
            });
            IContainer ioc = new DryIocServiceProviderFactory().CreateBuilder(services);
            IocManager.SetContainer(ioc);
            return services;
        }
    }
}
