using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Demo08_JsonConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            // configurationBuilder.Add(new JsonConfigurationSource()
            // {
            //     Path = "appsettings.json"
            // });
            configurationBuilder.AddJsonFile("appsettings.json");
            var configurationRoot = configurationBuilder.Build();
            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);
            Console.WriteLine(configurationRoot["section1:key3"]);
            Console.Read();
        }
    }
}