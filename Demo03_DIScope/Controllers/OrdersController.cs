using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Demo03_DIScope.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo03_DIScope.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IServiceProvider          _serviceProvider;
        private readonly IHostApplicationLifetime  _applicationLifetime;

        public OrdersController(ILogger<OrdersController> logger,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime applicationLifetime)
        {
            _logger              = logger;
            _serviceProvider     = serviceProvider;
            _applicationLifetime = applicationLifetime;
        }

        [HttpGet]
        public ActionResult GetOrders([FromServices] IOrderService service1,
            [FromServices] IOrderService service2)
        {
            // using (var serviceScope = Request.HttpContext.RequestServices.CreateScope())
            // {
            //     var orderService = serviceScope.ServiceProvider.GetService<IOrderService>();
            // }

            Console.WriteLine(_serviceProvider.GetHashCode());
            var orderService = _serviceProvider.GetService<IOrderService>();
            Console.WriteLine("=================================");

            return Ok();
        }

        [HttpGet("stop")]
        public void GetOrderServiceByApp()
        {
            _applicationLifetime.StopApplication();
        }
    }
}