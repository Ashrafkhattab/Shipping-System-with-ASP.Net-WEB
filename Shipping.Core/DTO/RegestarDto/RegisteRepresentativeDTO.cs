using Shipping.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.RegestarDto
{
    public class RegisteRepresentativeDTO : RegisterUserDto
    {

        [Required(ErrorMessage = "Amount is required.")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Display(Name = "Type")]
        public AmountType Type { get; set; }
        public int GovernorateId { get; set; }
    }
}
