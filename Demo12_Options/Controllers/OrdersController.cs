using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo12_Options.Controllers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo12_Options.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get([FromServices] IOrderService service) 
        {
            Console.WriteLine($"Max Count is {service.GetMaxOrderCount()}");
            return Ok();
        }
    }
}