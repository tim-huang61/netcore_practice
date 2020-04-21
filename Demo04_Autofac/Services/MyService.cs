using System;

namespace Demo04_Autofac.Services
{
    public class MyService : IMyService, IDisposable
    {
        public void ShowCode()
        {
            Console.WriteLine($"{nameof(MyService)}: {GetHashCode()}");
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }
    }
}