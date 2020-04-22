using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo13_ValidateOptions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo13_ValidateOptions
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
            services.AddScoped<IOrderService, OrderService>();
            services.AddOptions<OrderServiceOption>()
                .Bind(Configuration)
                .Services.AddSingleton<IValidateOptions<OrderServiceOption>, OrderServiceOptionValidator>();
                // .Validate(
                // options => options.MaxCount > 5000,
                // "have to be over 5000");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

    public class OrderServiceOptionValidator : IValidateOptions<OrderServiceOption>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOption options)
        {
            if (options.MaxCount > 4000)
            {
                return ValidateOptionsResult.Success;
            }

            return ValidateOptionsResult.Fail("have to be over 4000");
        }
    }

    public class OrderServiceOption
    {
        public int MaxCount { get; set; }
    }
}