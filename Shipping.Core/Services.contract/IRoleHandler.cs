using Microsoft.AspNetCore.Identity;
using Shipping.DTO;


namespace Shipping.Core.Services.contract
{
    public interface IRoleHandler
    {
        public Task<List<IdentityRole>> GetAllRole();
        public Task<IdentityResult> AddRoleAsync(RoleBaseDTO role);


        public Task<IdentityResult> EditRoleAsync(UpdateRole role);

        public Task<IdentityResult> DeleteRoleAsync(string id);

       public string GetErrorsDescription(IEnumerable<IdentityError> errors);
    }
}
