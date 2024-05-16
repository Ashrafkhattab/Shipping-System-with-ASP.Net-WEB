using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using Shipping.MiddlWares;
using System.Data;


namespace Shipping.Controllers
{

    public class RoleController : Controller
    {
        private readonly IRoleHandler _roleHandler;

        public RoleController(IRoleHandler roleHandler)
        {
            _roleHandler = roleHandler;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAll()
        {
            try
            {
                var result = await _roleHandler.GetAllRole();
                return Ok(result);
            }
            catch (ExceptionLogic ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddRole", Name = "AddRole")]
        public async Task<IActionResult> Add([FromBody] RoleBaseDTO role)
        {
            try
            {
                var result = await _roleHandler.AddRoleAsync(role);
                return Ok(role);
            }
            catch (ExceptionLogic ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("EditRole", Name = "EditRole")]
        public async Task<IActionResult> Edit([FromBody] UpdateRole role)
        {
            try
            {
                var result = await _roleHandler.EditRoleAsync(role);
                return Ok(role);
            }
            catch (ExceptionLogic ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            try
            {
                var result = await _roleHandler.DeleteRoleAsync(id);
                return Ok(result);
            }
            catch (ExceptionLogic ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
