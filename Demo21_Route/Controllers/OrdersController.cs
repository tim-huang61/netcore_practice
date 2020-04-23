using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Demo21_Route.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET
        [HttpGet("{id:isLong}")]
        public bool OrderExist([FromRoute] string id)
        {
            return true;
        }

        [HttpGet("{id:max(20)}")]
        public bool Max([FromRoute] long id, [FromServices] LinkGenerator linkGenerator)
        {
            var a = linkGenerator.GetPathByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new {name = "abc"});

            var uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new {name = "abc"});

            return true;
        }

        [HttpGet("{name:required}")]
        public bool Reque(string name)
        {
            return true;
        }

        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        public bool Number(string number)
        {
            return true;
        }
    }

    public class IsLongRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            return long.TryParse(values["id"].ToString(), out _);
        }
    }
}