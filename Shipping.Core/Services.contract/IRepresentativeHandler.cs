

using Shipping.DTO;
using Shipping.DTO.RegestarDto;
using Shipping.DTO.RepresentiveDTO;

namespace Shipping.Core.Services.contract
{
    public interface IRepresentativeHandler
    {
        public Task<List<GetAllRepresentaiveDTO>> GetAll();
        public void UpdateRepPassword(UpdatePasswordDtos upDto);
        public Task<int> UpdateRepresentativ(UpdateRepresentavieDTO upDTO);
        public void Delete(int id);
        public Task<ShowRepresentativeDTO> GetRepById(int id);
        public  Task<int> RegisteRepresentative(RegisteRepresentativeDTO registe);
        public Task<int> GetRepresentativeById(string appUserId);

    }
}
