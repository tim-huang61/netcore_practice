using System;
using Demo12_Options.Models;
using Microsoft.Extensions.Options;

namespace Demo12_Options.Services
{
    class OrderService : IOrderService
    {
        // 使用Options的設計, 主要為了只專注該類所使用的配置  
        // private readonly IOptions<OrderServiceOption> _option;
        //
        // public OrderService(IOptions<OrderServiceOption> option)
        // {
        //     _option = option;
        // }

        // IOptionsMonitor vs IOptionsSnapshot 
        // IOptionsSnapshot適用於Scoped, 在生命週期內則不會讀到最新配置的值
        // IOptionsMonitor適用於Singleton, 可持續讀取到最新的值
        // IOptionsSnapshot如果用於在Singleton的服務則會報錯 
        private readonly IOptionsMonitor<OrderServiceOption> _option;

        // 重新呼叫則會取配置的最新值
        public OrderService(IOptionsMonitor<OrderServiceOption> option)
        {
            _option = option;
            _option.OnChange(options => { Console.WriteLine(_option.CurrentValue.MaxCount); });
        }

        public int GetMaxOrderCount()
        {
            return _option.CurrentValue.MaxCount;
        }
    }
}