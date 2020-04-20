using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo02_DI.Models;
using Demo02_DI.Services;

namespace Demo02_DI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("api/services")]
        public ActionResult GetServices([FromServices] IMySingletonService singletonService1,
            [FromServices] IMySingletonService singletonService2,
            [FromServices] IMyTransientService transientService1,
            [FromServices] IMyTransientService transientService2,
            [FromServices] IMyScopedService scopedService1,
            [FromServices] IMyScopedService scopedService2)
        {
            Console.WriteLine($"singleton1: {singletonService1.GetHashCode()}");
            Console.WriteLine($"singleton2: {singletonService2.GetHashCode()}");
            Console.WriteLine($"transient1: {transientService1.GetHashCode()}");
            Console.WriteLine($"transient2: {transientService2.GetHashCode()}");
            Console.WriteLine($"scoped1: {scopedService1.GetHashCode()}");
            Console.WriteLine($"scoped2: {scopedService2.GetHashCode()}");

            return Content("OK");
        }

        [HttpGet("api/serviceList")]
        public ActionResult GetServiceList([FromServices] IEnumerable<IMyTransientService> services)
        {
            foreach (var service in services)
            {
                Console.WriteLine($"transient: {service.GetHashCode()}");
            }

            return Ok();
        }
        
        [HttpGet("api/genericService")]
        public ActionResult GetGenericService([FromServices] IGenericService<IMyTransientService> service)
        {
            return Ok();
        }
    }
}