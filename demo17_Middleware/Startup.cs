using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo17_Middleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                // 對Response操作, 再執行next會導致報錯
                // 原因是Header已經被改為唯讀, next已不能修改 
                // await context.Response.WriteAsync("Hello");
                await next();
                await context.Response.WriteAsync("Hello2");
            });

            app.Map("/abc",
                builder =>
                {
                    builder.Use(async (context, next) =>
                    {
                        // await next();
                        await context.Response.WriteAsync("Hello");
                    });
                });

            app.MapWhen(context => context.Request.Query.Keys.Contains("abc"),
                builder =>
                {
                    builder.Run(async context
                        =>
                    {
                        await context.Response.WriteAsync("new abc");
                    });
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}