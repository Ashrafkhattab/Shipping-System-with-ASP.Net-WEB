using Shipping.Core.Model.OrderAggregate;
using Shipping.DTO.Products;
using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO
{
    public class AddOrderDto
    {
        [Required(ErrorMessage = "MerchantId is required.")]
        public int MerchantId { get; set; }

        [Required(ErrorMessage = "Order type is required.")]
        [Range(1, 2)]
        public OrderType orderType { get; set; }

        [Required(ErrorMessage = "Payment Type is required.")]
        [Range(1, 3)]
        public TypeOfPaying PaymentType { get; set; }


        [Required(ErrorMessage = "Client name is required.")]
        public string ClientName { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string FirstPhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string SecondPhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Governorate is required.")]
        public int GovernorateId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;
        public bool DeliverToVillage { get; set; }

        [Required(ErrorMessage = "Shipping Type is required.")]
        [Range(1, 3)]
        public DeliveryMethode TypeOfDelevery { get; set; }

        [Required(ErrorMessage = "Branch is required.")]
        public int BranchId { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Enter at least one Product.")]
        public List<ProductDTO> Products { get; set; }
    }
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "Order Id is required.")]
        public int Id { get; set; }


        [Required(ErrorMessage = "MerchantId is required.")]
        public int MerchantId { get; set; }

        [Required(ErrorMessage = "Order type is required.")]
        [Range(0, 1)]
        public OrderType orderType { get; set; }

        [Required(ErrorMessage = "Client name is required.")]
        public string ClientName { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string FirstPhoneNumber { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //public string SecondPhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Governorate is required.")]
        public int GovernorateId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;
        public bool DeliverToVillage { get; set; }

        [Required(ErrorMessage = "Shipping Type is required.")]
        [Range(1,3)]
        public DeliveryMethode TypeOfDelevery { get; set; }

        [Required(ErrorMessage = "Payment Type is required.")]
        [Range(1, 2)]
        public TypeOfPaying PaymentType { get; set; }

        [Required(ErrorMessage = "Branch is required.")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public Status orderStatus { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Enter at least one Product.")]
        public List<ProductDTO> Products { get; set; }
    }
    public class ReadOrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Cost { get; set; }
        public Status orderStatus { get; set; }
    }
    public class ReadOrderReportsDto
    {
        public Status orderStatus { get; set; }
        public string Merchant { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string FirstPhoneNumber { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double ProductTotalCost { get; set; }
        public double OrderShippingTotalCost { get; set; }
        public DateTime Date { get; set; }
    }
    public class ReadAllOrderDataDto
    {
        public OrderType orderType { get; set; }

        public string ClientName { get; set; } = string.Empty;
        public string FirstPhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; } = string.Empty;
        public bool DeliverToVillage { get; set; }
        public string ShippingType { get; set; }
        public TypeOfPaying PaymentType { get; set; }
        public string Branch { get; set; }
        public string? Notes { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public record AddOrderResultDto(bool IsSuccesfull, double? ProductTotalCost, double? OrderShippingTotalCost, double? totalWeight);
    public record UpdateOrderResultDto(bool IsSuccesfull, double? ProductTotalCost, double? OrderShippingTotalCost, double? totalWeight);

}
