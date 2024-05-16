using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Controller : ControllerBase
    {
    }
}
