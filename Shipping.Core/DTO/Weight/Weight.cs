using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.Weight
{
    public class Weight
    {
        public class AddWeightDto
        {
            [Required(ErrorMessage = "DefaultWeight is required.")]
            public double DefaultWeight { get; set; }

            [Required(ErrorMessage = "AdditionalPrice is required.")]
            public double AdditionalPrice { get; set; }
        }

        public class UpdateWeightDto
        {
            [Required(ErrorMessage = "Id is required.")]
            public int Id { get; set; }

            [Required(ErrorMessage = "DefaultWeight is required.")]
            public double DefaultWeight { get; set; }

            [Required(ErrorMessage = "AdditionalPrice is required.")]
            public double AdditionalPrice { get; set; }
        }

    }
}
