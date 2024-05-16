using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Core.Model
{
    public class SpecialPrice : ModelBase
    {
        
        public decimal Price { get; set; }

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Merchant")]
        public int MerchentId { get; set; }
        public virtual Marchant? Merchant { get; set; }
    }
}
