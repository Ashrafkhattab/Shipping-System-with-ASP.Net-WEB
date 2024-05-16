using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using Shipping.DTO.RegestarDto;
using Shipping.DTO.RepresentiveDTO;


namespace Shipping.Controllers
{
  
    public class RepresentativeController : Controller
    {
        private readonly IRepresentativeHandler representativeHandler;

        public RepresentativeController(IRepresentativeHandler representativeHandler)
        {
            this.representativeHandler = representativeHandler;
        }
        [HttpPost]
        
        public async Task<ActionResult> RegisterRepresentative([FromBody] RegisteRepresentativeDTO registrationDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await representativeHandler.RegisteRepresentative(registrationDTO);
          if (result == 0) { return StatusCode(500); }
            return Ok(new { message = "Representative was added successfully." });

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult> GetAll()
        {
            var Representatives = await representativeHandler.GetAll();
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(Representatives);

        }

        [HttpDelete("Delete")]
       
        public async Task<IActionResult> DeleteRepresentative(int id)
        {
            if(!ModelState.IsValid) { return BadRequest(); }
             representativeHandler.Delete(id);
            return Ok("Deleted");
        }

        [HttpPut("pass")]
        public async Task<IActionResult> UpdateRepresentativePass(string id, UpdatePasswordDtos updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            representativeHandler.UpdateRepPassword(updateDto);
            
                return Ok();
            
        }


        [HttpPut("UpdateRepresentative")]
       
        public async Task<IActionResult> UpdateRepresentative(int id, [FromBody] UpdateRepresentavieDTO updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            representativeHandler.UpdateRepresentativ(updateDto);
                return Ok("Update is done");
            
        }

        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetRepresentativeById(int id)
        {
            var representative = await representativeHandler.GetRepById(id);
            if (representative == null)
                return NotFound();

            return Ok(representative);
        }

        [HttpGet("{appUserId}")]
        public async Task<ActionResult<int>> GetRepresentativeById(string appUserId)
        {
            var result = await representativeHandler.GetRepresentativeById(appUserId);
            if (result == 0)
                return NotFound();

            return Ok(new { id = result});
        }




    }
}
