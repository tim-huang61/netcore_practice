using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo13_ValidateOptions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo13_ValidateOptions.Controllers
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
        public void Get([FromServices] IOrderService service)
        {
        }
    }
}