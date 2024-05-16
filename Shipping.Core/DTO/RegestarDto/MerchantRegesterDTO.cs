using Shipping.DTO.SpecailPrices;
using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.RegestarDto
{
    public class MerchantRegesterDTO : RegisterUserDto
    {
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        [Required(ErrorMessage = "StoreName is required.")]
        public string StoreName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ReturnerPercent is required.")]
        [Range(0, 100, ErrorMessage = "ReturnerPercent must be a percentage between 0 and 100.")]
        public double ReturnerPercent { get; set; }
        public List<SpecialPricesDTO> SpecialPrices { get; set; }

    }
}
