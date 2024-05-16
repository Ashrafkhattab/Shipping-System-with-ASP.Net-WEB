using Shipping.Core.Model;

namespace Shipping.DTO.RepresentiveDTO
{
    public class UpdateRepresentavieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int BranchId { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public AmountType Type { get; set; }

      
    }
}
