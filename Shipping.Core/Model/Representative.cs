using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Core.Model
{
  
    public class Representative : ModelBase
    {
        
        public decimal? Amount { get; set; }
        public AmountType? Type { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Governorate")]
        public int? GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }
    }
    public enum AmountType
    {
        Percent,
        Fixed
    }
}
