
using Shipping.DTO;
using Shipping.DTO.Merchant;
using Shipping.DTO.RegestarDto;

namespace Shipping.Core.Services.contract
{
    public interface IMerchantHandler
    {
        public Task<List<GetAllMerchantsDto>> GetAllMarchentsAsync();
        Task<int> RegisterMerchant(MerchantRegesterDTO registrationDTO);
        Task<int> UpdateMerchantPassword(UpdatePasswordDtos updatePassDto);
        Task<int> UpdateMerchant(MerchantUpdateDto updateDto);
        Task<int> DeleteMerchant(int userId);
        Task<(int,string,string)> GetMerchantBystringId(string userId);
        Task<MerchantUpdateDto> GetMerchantByIdWithSpecialPrices(int merchantId);
    }
}
