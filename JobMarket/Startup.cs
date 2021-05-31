using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMarket.API;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using static System.Environment;

namespace JobMarket
{
    public class Startup
    {
        public Startup(IHostEnvironment environment,
            IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        private IHostEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "JobAds",
                        Version = "v1"
                    }));

            services.AddSingleton(new JobAdsApplicationService());
        }
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "JobAds v1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
