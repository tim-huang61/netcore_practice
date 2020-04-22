using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Demo06_ConfigurationCommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            // switchMappings ：針對key去做替換模式
            // "-" 就必須找別名
            builder.AddCommandLine(args, new Dictionary<string, string>
            {
                {"-k1", "CommandKey1"}
            });

            var configurationRoot = builder.Build();
            // 在launchSetting.json k1被指向CommandKey1
            Console.WriteLine(configurationRoot["CommandKey1"]);
            Console.WriteLine(configurationRoot["CommandKey2"]);

            Console.Read();
        }
    }
}