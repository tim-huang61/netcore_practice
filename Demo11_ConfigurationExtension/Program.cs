using System;
using System.ComponentModel;
using System.Globalization;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Demo11_ConfigurationExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddMyConfigurationSource();
            var configurationRoot = builder.Build();
            ChangeToken.OnChange(configurationRoot.GetReloadToken,
                () => Console.WriteLine(configurationRoot["lastTime"]));
            Console.Read();
        }
    }

    public class MyConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyConfigurationProvider();
        }
    }

    internal class MyConfigurationProvider : ConfigurationProvider
    {
        public MyConfigurationProvider()
        {
            var timer = new Timer();
            timer.Elapsed  += (sender, args) => Load(true);
            timer.Interval =  3000;
            timer.Start();
        }

        public override void Load()
        {
            Load(false);
        }

        private void Load(bool isReload)
        {
            Data["lastTime"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            if (isReload)
            {
                base.OnReload();
            }
        }
    }
}