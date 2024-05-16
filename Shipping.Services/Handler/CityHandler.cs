
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO.City;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shipping.Services.Handler
{
    public class CityHandler : ICityHandler
    {
        private readonly ICityReprosatriy reprosatriy;
        private readonly IGovernorateRepository governorateRepository;

        public CityHandler(ICityReprosatriy reprosatriy ,IGovernorateRepository governorateRepository)
        {
            this.reprosatriy = reprosatriy;
            this.governorateRepository = governorateRepository;
        }
        public List<ShowCityDTO> Gettall()
        {
            return reprosatriy.GetAll();
        }
        public UpdateCityDto GetById(int id)
        {
            var city = reprosatriy.GetById(id);
            if (city == null)
            {
                return null;
            }
            return new UpdateCityDto
            {
                Id = city.Id,
                Name = city.Name,
                Pickup = city.Pickup,
                Price = city.Price,
                GovernorateId = city.GovernorateId
            };
        }
        public List<ShowCityDropDwon> GetCityDropDwons()
        {
            return reprosatriy.GettallShowDrop();
        }
        public void AddCity(AddCityDto cityDto)
        {
            var governorate = governorateRepository.GetById(cityDto.GovernorateId);
            var city = reprosatriy.GetByName(cityDto.Name);

            if (governorate == null)
            {
                throw new Exception("Governorate not found");
            }
            else if (cityDto.Name== city.Name) { throw new Exception("City Already Exsite"); }

            reprosatriy.Add(cityDto);
            reprosatriy.SaveChanges();
        }
        public void Update(UpdateCityDto cityDto)
        {
            var city = reprosatriy.GetByName(cityDto.Name);
             if (cityDto.Name == city.Name)
            { throw new Exception("City Already Exsite"); }

            reprosatriy.Update(cityDto);
            reprosatriy.SaveChanges();
        }
        public void DeletCity(int  cityId)
        {
            reprosatriy.Delete(cityId);
            reprosatriy.SaveChanges();
        }

    }
}
