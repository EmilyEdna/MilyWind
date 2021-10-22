using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Mily.Wind.Extens.SystemConfig.SystemExten;
using Mily.Wind.VMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mily.Wind.LogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.RegisterLogConfiguration();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(opt =>
            {
                opt.TagActionsBy(t => new string[] { t.HttpMethod });
                opt.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
                opt.SwaggerDoc("Logs", new OpenApiInfo { Title = "日志服务" });
                ACStatic.AC002.ForEach(item =>
                {
                    opt.IncludeXmlComments(item, false);
                });
            });

            services.RegistLog();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint($"/swagger/Logs/swagger.json", "日志服务");
                });
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
