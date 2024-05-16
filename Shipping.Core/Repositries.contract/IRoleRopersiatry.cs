using Microsoft.AspNetCore.Identity;
using Shipping.DTO;


namespace Shipping.Core.Repositries.contract
{
    public interface IRoleRopersiatry
    {
        Task<IdentityResult> AddRoleAsync(RoleBaseDTO role);
        Task<IdentityResult> EditRoleAsync(UpdateRole role);
        Task<IdentityResult> DeleteRoleAsync(string id);
        Task<List<IdentityRole>> GetallRoll();
    }
}
