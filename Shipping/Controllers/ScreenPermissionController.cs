using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shapping.Handler;
using Shipping.DTO;
using Shipping.Services.Handler;


namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenPermissionController : ControllerBase
    {
        private readonly ScreenPermissionHandler _handler;

        public ScreenPermissionController(ScreenPermissionHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] string roleName)
        {
            var permissions = await _handler.GetPermissions(roleName);
            return Ok(permissions);
        }

        [HttpPut("UpdatePermission")]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionScreensRequestDTO permission)
        {
            await _handler.UpdatePermission(permission);
            return Ok();
        }
    }
}
