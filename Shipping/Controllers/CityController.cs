using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shapping.Handler.City;
using Shipping.Core.Services.contract;
using Shipping.DTO.City;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityHandler cityHandler;

        public CityController(ICityHandler cityHandler) 
        {
            this.cityHandler = cityHandler;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowCityDTO>>> GetAllCities()
        {
            var cities =  cityHandler.Gettall();
            if(cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }
        [HttpGet("Id")]
        public async Task<ActionResult> GetById(int id)
        {
           var city = cityHandler.GetById(id);
            return Ok(city);
        } 
        [HttpPost("Creat")]
        public async Task<ActionResult> CreateCity(AddCityDto cityDto)
        {
            cityHandler.AddCity(cityDto);   
            return Ok();
        }
        [HttpPut("UpdateCity")]
        public async Task<ActionResult> UpdateCity(UpdateCityDto cityDto)
        {
            if(cityDto == null)
            {
                return NotFound();
            }
            cityHandler.Update(cityDto);
            return Ok();
        }
        [HttpDelete("Id")]
        public async Task <ActionResult> DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            cityHandler.DeletCity(id);
            return Ok();
        }

    }
}
