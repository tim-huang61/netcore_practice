using Demo18_Exception.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Demo18_Exception.Controllers
{
    public class ErrorController : ControllerBase
    {
        // GET
        [Route("error")]
        public ActionResult Get()
        {
            IKnownException knownException;
            var            exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var            exception               = exceptionHandlerFeature?.Error;
            if (exception is IKnownException ex)
            {
                knownException = ex;
            }
            else
            {
                //未知錯誤必須記錄log
                knownException = KnownException.UnKnow();
            }

            return Content(knownException.ToString());
        }
    }
}