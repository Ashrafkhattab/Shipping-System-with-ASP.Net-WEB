using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Core.Model
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Address { get; set; } = string.Empty;

        [ForeignKey("branch")]
        public Nullable<int> BranchId { get; set; }
        public virtual Branches? branch { get; set; }

        [Required]
        public string Name { get; set; } 
        public bool IsDeleted { get; set; } = false;

        public Marchant? Marchant { get; set; }
        public Representative? Representative { get; set; }
    }
}
