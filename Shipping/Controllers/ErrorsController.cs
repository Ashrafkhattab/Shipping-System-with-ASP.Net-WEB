using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Errors;

namespace Shipping.Controllers
{
    [Route("error/code")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult  Error (int code)
        {
            return NotFound(new ApiResponse(code), "");
        }
    }
}
