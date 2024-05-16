
using Shipping.Core.Model;

namespace Shipping.DTO.RepresentiveDTO
{
    public class GetAllRepresentaiveDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BranchName { get; set; }
        public decimal? Amount { get; set; }
        public AmountType? Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}
