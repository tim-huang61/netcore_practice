using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Demo09_ConfigurationChangeToken
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true, true);
            var configurationRoot = configurationBuilder.Build();

            // 最佳實作
            ChangeToken.OnChange(configurationRoot.GetReloadToken, () =>
            {
                Console.WriteLine(configurationRoot["key1"]);
                Console.WriteLine(configurationRoot["key2"]);
                Console.WriteLine(configurationRoot["section1:key3"]);
            });

            // 只能執行一次, 必須在callback重複註冊
            // var reloadToken = configurationRoot.GetReloadToken();
            // reloadToken.RegisterChangeCallback(state =>
            // {
            //
            // }, configurationRoot);

            Console.Read();
        }
    }
}