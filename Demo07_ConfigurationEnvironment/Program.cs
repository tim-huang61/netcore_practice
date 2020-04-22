using System;
using Microsoft.Extensions.Configuration;

namespace Demo07_ConfigurationEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();

            var configurationRoot = builder.Build();
            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);
            Console.WriteLine(configurationRoot["section1:key3"]);
            Console.WriteLine(configurationRoot["tim_key1"]);

            // 環境變數加上前綴
            builder.AddEnvironmentVariables("tim_");
            var root = builder.Build();
            Console.WriteLine(root["key1"]);

            Console.Read();
        }
    }
}