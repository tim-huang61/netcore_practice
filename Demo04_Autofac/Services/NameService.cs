using System;

namespace Demo04_Autofac.Services
{
    public class NameService : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{nameof(NameService)}: dispose");
        }
    }
}