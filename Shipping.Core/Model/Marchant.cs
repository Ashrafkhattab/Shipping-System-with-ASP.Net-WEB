using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Core.Model
{
    public class Marchant : ModelBase
    {
            
        public string? StoreName { get; set; }

        public double? ReturnerPercent { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        public virtual City? City { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Governorate")]
        public int? GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }
        public ICollection<SpecialPrice> SpecialPrices { get; set; } = new HashSet<SpecialPrice>();


    }
}
