using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace Demo16_Serilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(Configuration)
                // .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.File(new CompactJsonFormatter(), "logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IConfiguration Configuration =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    optional: true)
                // .AddEnvironmentVariables()
                .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog(dispose: true);
    }
}