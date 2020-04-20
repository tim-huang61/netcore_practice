using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo01_Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 基本的生命週期
        /// 1. ConfigureWebHostDefaults => 2. ConfigureHostConfiguration => 3. ConfigureAppConfiguration
        /// 4. Startup.ConfigureServices => 5. ConfigureLogging => 6. ConfigureServices 
        /// 7. Startup.Configure
        /// Startup.ConfigureServices、ConfigureServices會依ConfigureWebHostDefaults註冊的順序來決定先後
        /// 如果WebHost在最後的話，則Startup.ConfigureServices與ConfigureServices顛倒順序
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureHostConfiguration(builder => { Console.WriteLine("ConfigureHostConfiguration"); })
                .ConfigureAppConfiguration(builder => { Console.WriteLine("ConfigureAppConfiguration"); })
                .ConfigureLogging(config => Console.WriteLine("ConfigureLogging"))
                .ConfigureServices(collection => Console.WriteLine("ConfigureServices"));
    }
}