using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mily.Wind.Extens.InternalInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Extens.SystemConfig
{
    public static class MilyServiceCollection
    {
        public static IConfiguration RegisterConfiguration(this IConfiguration configuration)
        {
            Caches.RedisConnectionString = configuration["CacheConfig:Redis"];
            Caches.DbName = configuration["CacheConfig:MogoName"];
            Caches.MongoDBConnectionString = configuration["CacheConfig:Mogo"];
            return configuration;
        }

        public static IServiceCollection RegisterService(this IServiceCollection services)
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
            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                opt.SerializerSettings.Converters.Add(new MilyJsonConvert());
            });

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

            services.RegistIoc();

            return services;
        }

        public static IServiceCollection RegistIoc(this IServiceCollection services)
        {
            IContainer ioc = new DryIocServiceProviderFactory().CreateBuilder(services);
            var AllAssemblies = SyncStatic.Assembly("Mily.Wind");

            var LogicServices = AllAssemblies.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ILogic)))).ToList();

            LogicServices.ForEach(item =>
            {
                if (item.IsClass)
                {
                    var interfaces = item.GetInterfaces().Where(imp => imp.GetInterfaces().Contains(typeof(ILogic))).FirstOrDefault();
                    var impl = Activator.CreateInstance(item).GetType();
                    ioc.Register(interfaces, impl, Reuse.Transient);
                }
            });
           
            MilyUtily.SetContainer(ioc);
            return services;
        }
    }
}
