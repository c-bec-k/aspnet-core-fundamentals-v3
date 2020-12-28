using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnet_core_fundamentals_v3
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration config)
        {
            Config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, ConfigurationGreeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("I can't let you do that, Dave.")
                });
            }

            app.UseWelcomePage(new WelcomePageOptions { Path = "/welcome" });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    throw new Exception("Something went wrong");
                    await context.Response.WriteAsync(greeter.GetGreeting());
                });
            });
        }
    }
}
