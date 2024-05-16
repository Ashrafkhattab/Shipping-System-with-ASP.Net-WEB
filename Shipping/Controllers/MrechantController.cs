using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.contract;
using Shipping.DTO;
using Shipping.DTO.Merchant;
using Shipping.DTO.RegestarDto;


namespace Shipping.Controllers
{

    public class MrechantController : Controller
    {
        private readonly IMerchantHandler merchantHandler;

        public MrechantController(IMerchantHandler merchantHandler)
        {
            this.merchantHandler = merchantHandler;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMerchant(MerchantRegesterDTO registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await merchantHandler.RegisterMerchant(registrationDto);

            if (result > 0)
            {
                return Ok(new { message = "Merchant was added successfully." });
            }
            return StatusCode(500);

        }


        [HttpPut]
        public async Task<IActionResult> UpdateMerchant(int id, MerchantUpdateDto updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await merchantHandler.UpdateMerchant(updateDto);

            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMerchant(int id)
        {
            var result = await merchantHandler.DeleteMerchant(id);

            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllMerchants()
        {
            var merchants = await merchantHandler.GetAllMarchentsAsync();
            return Ok(merchants);
        }



        [HttpPut("pass")]
        public async Task<IActionResult> UpdateMerchantPass(string id, UpdatePasswordDtos updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await merchantHandler.UpdateMerchantPassword(updateDto);

            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }



        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetMerchantById(int id)
        {
            var merchant = await merchantHandler.GetMerchantByIdWithSpecialPrices(id);

            if (merchant == null)
            {
                return NotFound();
            }

            return Ok(merchant);
        }


        [HttpGet("{appUserId}")]
        public async Task<ActionResult<(int, string, string)>> GetMerchantDetails(string appUserId)
        {
            var result = await merchantHandler.GetMerchantBystringId(appUserId);
            if (result.Item1 == 0)
                return NotFound();

            return Ok(new { id = result.Item1, phone = result.Item2, adress = result.Item3 });
        }







    }
}
