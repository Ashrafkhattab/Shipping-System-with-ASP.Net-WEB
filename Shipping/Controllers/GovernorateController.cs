using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.contract;
using Shipping.DTO.Governoret;


namespace Shipping.Controllers
{

    public class GovernorateController : Controller
    {
        private readonly IGovernorateHandler goverHandler;

        public GovernorateController(IGovernorateHandler goverHandler)
        {
            this.goverHandler = goverHandler;
        }

        [HttpGet("GetAllWithDelete")]
        public ActionResult GetAllWithDelete()
        {
           var govers = goverHandler.GetAllWithDelet();
            if(govers == null) { return NotFound(); }
            return Ok(govers);
        }
        [HttpGet("GetAllwithCities")]
        public ActionResult GetAllwithCities()
        {
            var govers = goverHandler.GetAllWithCityDropdown(); 
            if(govers == null) { return NotFound(); }
            return Ok(govers);
        }

        [HttpPost("AddGovern")]
        public ActionResult AddGovern(AddGovernorateDTO governorateDTO)
        {
            if (governorateDTO == null) { return BadRequest(); }
            goverHandler.AddGovern(governorateDTO);
            return Ok();
        }

        [HttpPut("UpdateGov")]
        public  ActionResult UpdateGov(UpdateGovernorateDTO upDto)
        {
            if(upDto == null) { return BadRequest(); }
            goverHandler.Update(upDto);
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteGovern(int id)
        {
            if(id == 0) { return BadRequest(); }
            goverHandler.Delete(id);
            return Ok();
        }
        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {
            if(id == 0) { return BadRequest(); }
           var gov= goverHandler.GetById(id);
            return Ok(gov);
        }


    }
}
