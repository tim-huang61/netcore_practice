using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Demo04_Autofac.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo04_Autofac
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
            var autofacRoot = app.ApplicationServices.GetAutofacRoot();
            var service     = autofacRoot.ResolveNamed<IMyService>("service2");
            service.ShowCode();
            // using (var scope = autofacRoot.BeginLifetimeScope("myScope"))
            // {
            //     var service = scope.ResolveNamed<IMyService>("service2");
            //     service.ShowCode();
            // }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MyService>().As<IMyService>()
                .InterceptedBy(typeof(MyInterceptor))
                .EnableInterfaceInterceptors();

            builder.RegisterType<MyService2>()
                .Named<IMyService>("service2")
                .PropertiesAutowired();
            builder.RegisterType<NameService>();
            // builder.RegisterType<NameService>().InstancePerMatchingLifetimeScope("myScope");
            builder.RegisterType<MyInterceptor>();
        }
    }

    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("before");
            invocation.Proceed();
            Console.WriteLine("after");
        }
    }
}