using System;
using Autofac.Features.OwnedInstances;

namespace Demo04_Autofac.Services
{
    public class MyService2 : IMyService
    {
        public NameService NameService { get; set; }

        public void ShowCode()
        {
            Console.WriteLine($"NameService是否為空：{NameService == null}");
        }
    }
}