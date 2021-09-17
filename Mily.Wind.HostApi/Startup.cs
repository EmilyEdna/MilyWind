using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
using System;
using System.IO;

namespace Mily.Wind.HostApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.RegisterConfiguration();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterService();
            services.AddSwaggerGen(c =>
            {
                c.TagActionsBy(t => new string[] { t.HttpMethod });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
                foreach (var item in ACStatic.AC001)
                {
                    c.SwaggerDoc(item.Key, new OpenApiInfo { Title = item.Value });
                }
                c.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme{  Reference = new OpenApiReference{  Type= ReferenceType.SecurityScheme, Id="Bearer"} },
                      Array.Empty<string>()
                    }
                });
                ACStatic.AC002.ForEach(item =>
                {
                    c.IncludeXmlComments(item, false);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseKnife4UI(c =>
                {
                    c.RoutePrefix = ""; // serve the UI at root
                    foreach (var item in ACStatic.AC001.Keys)
                    {
                        c.SwaggerEndpoint($"/swagger/{item}/swagger.json", item);
                    }
                });
                //app.UseSwaggerUI(c =>
                //{
                //    foreach (var item in ACStatic.AC001.Keys)
                //    {
                //        c.SwaggerEndpoint($"/swagger/{item}/swagger.json", item);
                //    }
                //});
            }
            app.UseStaticFiles();

            app.UseMiddleware<MilyMiddleWare>();

            app.UseRouting();
            //认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
