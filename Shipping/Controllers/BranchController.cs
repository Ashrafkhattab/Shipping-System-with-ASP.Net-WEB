using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.contract;
using Shipping.DTO.Branch;

namespace Shipping.Controllers
{

    public class BranchController : Controller
    {
        private readonly IBranchesHandler branchesHandler;

        public BranchController(IBranchesHandler  branchesHandler)
        {
            this.branchesHandler = branchesHandler;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<getBranchByIdDto>> GetById(int id)
        {
            var branch =  branchesHandler.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<getBranchByIdDto>>> GetAll()
        {
            var branch = branchesHandler.GetBranches();
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddBranchDto branchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            branchesHandler.CreateBranchAsync(branchDto);
            return Ok();
        }
        [HttpPut("UpdateShipping")]
        public async Task<IActionResult> Update(int id, UpdateBranchDto branchDto)
        {
            if (branchDto.Id != id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            branchesHandler.UpdateBranchAsync(branchDto);
                return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            branchesHandler.DeleteBranchAsync(id);
            return Ok(new { Message = "Branch Deleted Successfully" });
        }
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            branchesHandler.ChangeStatusBranchAsync(id);
                return Ok();
        }

    }
}
