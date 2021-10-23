using DotNetCore.CAP;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.LogPlugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text;
using XExten.Advance.AopFramework;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Extens.SystemConfig.SystemExten
{
    public static class MilyServiceCollection
    {
        private static IConfiguration Configuration { get; set; }
        public static IConfiguration RegisterConfiguration(this IConfiguration configuration)
        {
            Caches.RedisConnectionString = configuration.GetConnectionString("Redis");
            Caches.DbName = configuration.GetConnectionString("MongoName");
            Caches.MongoDBConnectionString = configuration.GetConnectionString("Mongo");
            Configuration = configuration;
            return configuration;
        }

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.RegistApi();

            services.RegistJwt();

            services.RegistCap();

            services.RegistIoc();

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
                opt.Filters.Add(typeof(MilyFilter));
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

            //2.找出默认的Activator
            var DefaultActivator = services.FirstOrDefault(m => m.ServiceType == typeof(IControllerActivator));
            //3.移除默认Activator
            services.Remove(DefaultActivator);
            //4.把自己自定义的Activator添加进去
            services.AddTransient<IControllerActivator, MilyControllerActivator>();

            return services;
        }

        public static IServiceCollection RegistJwt(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "MilyWind",
                    ValidIssuer = "MilyWind",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This MilyWind is the latest micro service project .")),
                };
            });
            return services;
        }

        public static IServiceCollection RegistCap(this IServiceCollection services)
        {
            services.AddCap(opt =>
            {
                opt.UseMongoDB(Configuration.GetConnectionString("Mongo"));
                opt.UseRabbitMQ(t =>
                {
                    t.HostName = Configuration["RabbitMQ:HostName"];
                    t.Port = Configuration["RabbitMQ:Port"].AsInt();
                    t.VirtualHost = Configuration["RabbitMQ:VirtualHost"];
                    t.UserName = Configuration["RabbitMQ:Name"];
                    t.Password = Configuration["RabbitMQ:Pwd"];
                });
                opt.UseDashboard();
            });
            return services;
        }

        public static IServiceCollection RegistIoc(this IServiceCollection services)
        {

            var AllAssemblies = SyncStatic.Assembly("Mily.Wind");

            var LogicServices = AllAssemblies.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ILogic)))).ToList();
            var CapServices = AllAssemblies.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ICapSubscribe)))).ToList();
            //Cap
            CapServices.ForEach(item =>
            {
                if (item.IsClass)
                {
                    var interfaces = item.GetInterfaces().Where(imp => imp.GetInterfaces().Contains(typeof(ICapSubscribe))).FirstOrDefault();
                    services.AddSingleton(interfaces, item);
                }
            });
            //服务
            LogicServices.ForEach(item =>
            {
                if (item.IsClass)
                {
                    var interfaces = item.GetInterfaces().Where(imp => imp.GetInterfaces().Contains(typeof(ILogic))).FirstOrDefault();
                    services.AddSingleton(interfaces, AopProxy.CreateProxyOfRealize(interfaces, item).GetType());
                }
            });

            services.AddMilyLog();

            IContainer ioc = new DryIocServiceProviderFactory().CreateBuilder(services);
            IocManager.SetContainer(ioc);
            return services;
        }
    }
}
