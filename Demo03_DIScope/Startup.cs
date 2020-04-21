using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo03_DIScope.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo03_DIScope
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
            // 根容器只會釋放有實作IDisposable, 其他則會交由GC處理
            services.AddControllers();
            services.AddTransient<IOrderService, OrderService>();
            // services.AddScoped<IOrderService, OrderService>();
            // services.AddSingleton<IOrderService, OrderService>();

            // singleton如不是透過根容器建立的實體，在application結束的時候是不會被釋放的 
            // 單純直接傳物件的話會有這樣的問題
            // services.AddSingleton<IOrderService>(new OrderService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 瞬時服務如果在根容器取得, 是必須到application結束才會被釋放(坑)
            var orderService = app.ApplicationServices.GetService<IOrderService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}