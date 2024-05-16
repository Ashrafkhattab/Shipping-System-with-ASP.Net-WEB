

using Shipping.Core.Model;

namespace Shipping.DTO.RepresentiveDTO
{
    public class ShowRepresentativeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public AmountType Type { get; set; }
      public  bool IsDeleted { get; set; } = false;
    }
}
