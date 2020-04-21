using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo04_Autofac.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo04_Autofac.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get([FromServices] IMyService service)
        {
            service.ShowCode();
        }
    }
}