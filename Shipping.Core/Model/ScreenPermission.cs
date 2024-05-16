using Microsoft.AspNetCore.Identity;

namespace Shipping.Core.Model
{
    public class ScreenPermission : ModelBase
    {
        
        public bool Get { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public int ScreenId { get; set; }
        public string RoleId { get; set; }

        public Screen Screen { get; set; }
        public IdentityRole Role { get; set; }
    }
}
