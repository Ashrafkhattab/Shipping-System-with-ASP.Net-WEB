using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO.Governoret;
using Shipping.MiddlWares;

namespace Shipping.Services.Handler
{
    public class GovernorateHandler:IGovernorateHandler
    {
        private readonly IGovernorateRepository governorateRepository;

        public GovernorateHandler(IGovernorateRepository governorateRepository)
        {
            this.governorateRepository = governorateRepository;
        }

        public List<ShowGovernorateDTO> GetAllWithDelet()
        {    var govers = governorateRepository.GetAllWithDelete();
            if(govers == null) { throw new ExceptionLogic(""); }
            return govers;
        }

        public List<ShowGovernorateWithCityDropdownDTO> GetAllWithCityDropdown()
        {
            var govers = governorateRepository.GetAll();
            if (govers == null) { throw new ExceptionLogic(""); }
            return govers;
        }

        public void AddGovern(AddGovernorateDTO governorateDTO)
        {
            var gover = governorateRepository.GetByName(governorateDTO.Name);
            if(governorateDTO == null) { throw new ExceptionLogic("Name is Required"); }
            else if (governorateDTO.Name == gover.Name ) { throw new ExceptionLogic("Name is Existed"); }
            governorateRepository.Add(governorateDTO);
            governorateRepository.SaveChanges();
        }

        public void Update(UpdateGovernorateDTO upDto)
        {
            var gover = governorateRepository.GetByName(upDto.Name);

            if (upDto == null) { throw new ExceptionLogic("Name is Required"); }
            else if (upDto.Name == gover.Name) { throw new ExceptionLogic("Name is Existed"); }

            governorateRepository.Update(upDto);
            governorateRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            if(id == 0) { throw new ExceptionLogic(""); }
            governorateRepository.Delete(id);
            governorateRepository.SaveChanges();
        }


        public ReadGovernorateDTO GetById(int id)
        {
          return  governorateRepository.GetGovernorateWithCities(id);
        }


    }
}
