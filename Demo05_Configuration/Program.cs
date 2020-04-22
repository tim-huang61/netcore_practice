using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Demo05_Configuration
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"section1:key3", "value3"},
                {"section2:subSection1:key4", "value4"}
            });
            var configurationRoot = builder.Build();

            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);
            Console.WriteLine(configurationRoot["section1:key3"]);
            Console.WriteLine(configurationRoot.GetSection("section1")["key3"]);
            Console.WriteLine(configurationRoot["section2:subSection1:key4"]);

            Console.Read();
        }
    }
}