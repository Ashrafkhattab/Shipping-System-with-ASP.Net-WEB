using Shipping.DTO.SpecailPrices;

namespace Shipping.DTO.Merchant
{
    public class MerchantUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StoreName { get; set; }
        public double? PickUp { get; set; }
        public double ReturnerPercent { get; set; }
        public int BranchId { get; set; }
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        public List<SpecialPricesDTO> SpecialPrices { get; set; }
    }
}
