using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo14_LoggingSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configurationRoot = builder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddSingleton<IConfiguration>(provider => configurationRoot);
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configurationRoot.GetSection("Logging"));
                loggingBuilder.AddConsole();
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            // 一般不會使用LoggerFactory來使用
            // var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            // var logger        = loggerFactory.CreateLogger("alogger");
            // logger.LogDebug("alogger debug");
            // logger.LogInformation("alogger information");

            var orderService = serviceProvider.GetService<IOrderService>();

            Console.Read();
        }
    }

    internal interface IOrderService
    {
    }

    class OrderService : IOrderService
    {
        // 一般會使用這樣的方式來定義logger, 如此一來可以透過配置來決定該類紀錄log的等級
        public OrderService(ILogger<OrderService> logger)
        {
            logger.LogInformation("GO GO GO");
        }
    }
}