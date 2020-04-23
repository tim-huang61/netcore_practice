using System;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;

namespace Demo19_StaticFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        // GET
        public ActionResult Get()
        {
            return Ok(new[]
            {
                new {Id = 1},
                new {Id = 2},
                new {Id = 3}
            });
        }
    }
}