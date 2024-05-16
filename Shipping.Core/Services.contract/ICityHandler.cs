

using Shipping.DTO.City;

namespace Shipping.Core.Services.contract
{
    public interface ICityHandler
    {
        public List<ShowCityDTO> Gettall();
        public List<ShowCityDropDwon> GetCityDropDwons();
        public void AddCity(AddCityDto cityDto);
        public void Update(UpdateCityDto cityDto);
        public void DeletCity(int cityId);
        public UpdateCityDto GetById(int id);
    }
}
