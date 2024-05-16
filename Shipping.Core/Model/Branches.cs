using Shipping.Core.Model.OrderAggregate;

namespace Shipping.Core.Model
{
    public class Branches :ModelBase
    {
            
        public string Name { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool status { get; set; } = true;
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<AppUser> ApplicationUsers { get; set; } = new HashSet<AppUser>();
    }
}
