using Microsoft.Extensions.Options;

namespace Demo13_ValidateOptions.Services
{
    class OrderService : IOrderService
    {
        private readonly IOptionsSnapshot<OrderServiceOption> _option;

        public OrderService(IOptionsSnapshot<OrderServiceOption> option)
        {
            _option  = option;
            MaxCount = _option.Value.MaxCount;
        }

        public int MaxCount { get; set; }
    }
}