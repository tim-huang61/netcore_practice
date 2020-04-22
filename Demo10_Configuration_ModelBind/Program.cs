using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace Demo10_Configuration_ModelBind
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configurationRoot = builder.Build();
            // Microsoft.Extensions.Configuration.Binder
            var jsonConfig = new JsonConfig();
            configurationRoot.Bind(jsonConfig, options => { options.BindNonPublicProperties = true; });
            Console.WriteLine(jsonConfig.Key1);
            Console.WriteLine(jsonConfig.Key2);
            Console.WriteLine(jsonConfig.Section1.Key3);
            Console.WriteLine(jsonConfig.Key4);
        }
    }

    public class JsonConfig
    {
        public string Key4 { get; private set; } = "6666";

        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public Section Section1 { get; set; }
    }

    public class Section
    {
        public string Key3 { get; set; }
    }
}