using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo02_DI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Demo02_DI
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
            services.AddControllersWithViews();

            #region 一般註冊 有三種Singleton、Scoped、Transient

            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMyTransientService, MyTransientService>();

            #endregion

            #region 其他註冊

            // services.AddSingleton<IMySingletonService>(new MySingletonService());
            // services.AddSingleton<IMySingletonService>(provider =>
            // {
            //     // 可以依條件決定要哪種Service, ex: 工廠模式
            //     return new MySingletonService();
            // });

            // 另外兩種也適用

            #endregion

            #region 嘗試註冊 前面有註冊過就不會再註冊第二次

            // services.TryAddTransient<IMyTransientService, MyTransientService2>();
            // services.TryAddScoped<IMyScopedService, MyScopedService2>();
            // services.TryAddSingleton<IMyScopedService, MySingletonService2>();

            #endregion

            #region 嘗試註冊2 TryAddEnumerable 如果是不同的實作就可以再註冊

            // services.TryAddEnumerable(ServiceDescriptor.Transient<IMyTransientService, MyTransientService2>());

            #endregion

            #region 移除與替換

            // services.RemoveAll<IMyScopedService>();
            // services.Replace(ServiceDescriptor.Scoped<IMyScopedService, MyScopedService2>());

            #endregion

            #region 泛型註冊

            // singleton不能傳入scoped的註冊
            // services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}