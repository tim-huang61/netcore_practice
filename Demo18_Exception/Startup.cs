using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Demo18_Exception.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo18_Exception
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
            // services.AddMvc(options
            //     =>
            // {
            //     options.Filters.Add<MyExceptionFilter>();
            // }).AddJsonOptions(options =>
            // {
            //     options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 呈現錯誤的方式1 
            // 利用asp net core所提供的
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 畫面的方式呈現
            // app.UseExceptionHandler("/error");

            // app.UseExceptionHandler(builder =>
            // {
            //     builder.Run(async context =>
            //     {
            //         var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            //         var error                   = exceptionHandlerFeature.Error;
            //         var knowException           = error as IKnowException ?? UnKnowException.UnKnow();
            //         context.Response.StatusCode = knowException.ErrorCode != 99999 ? 400 : 500;
            //
            //         context.Response.ContentType = "application/json; charset=utf-8";
            //         var jsonOptions  = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //         var errorMessage = JsonSerializer.Serialize(knowException, jsonOptions.Value.JsonSerializerOptions);
            //         await context.Response.WriteAsync(errorMessage);
            //     });
            // });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class MyExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<MyExceptionFilter> _logger;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var knowException = context.Exception as IKnownException;
            context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            if (knowException == null)
            {
                // 紀錄log
                _logger.LogError(context.Exception.ToString());
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ;
                knowException = KnownException.UnKnow();
            }

            context.Result = new JsonResult(KnownException.FromKnownException(knowException))
            {
                ContentType = "application/json;charset=utf-8"
            };
        }
    }
}