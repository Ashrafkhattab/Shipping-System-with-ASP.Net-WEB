

using Shipping.Core.Model;
using Shipping.DTO.Governoret;

namespace Shipping.Core.Repositries.contract
{
    public interface IGovernorateRepository :IGenricRepository<Governorate>
    {
         List<ShowGovernorateDTO> GetAllWithDelete();
         Model.Governorate GetByName(String name);
         ReadGovernorateDTO GetGovernorateWithCities(int governorateId);
    }
}
