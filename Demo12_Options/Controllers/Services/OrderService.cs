using Demo12_Options.Models;
using Microsoft.Extensions.Options;

namespace Demo12_Options.Controllers.Services
{
    class OrderService : IOrderService
    {
        private readonly IOptions<OrderServiceOption> _option;

        public OrderService(IOptions<OrderServiceOption> option)
        {
            _option = option;
        }

        public int GetMaxOrderCount()
        {
            return _option.Value.MaxCount;
        }
    }
}