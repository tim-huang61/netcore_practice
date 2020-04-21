using System;

namespace Demo03_DIScope.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"OrderService Disposed: {this.GetHashCode()}");
        }
    }
}