using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.DTO;


namespace Shipping.Repositry.Repositories
{
    public class RoleRopersatriy : IRoleRopersiatry
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleRopersatriy(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddRoleAsync(RoleBaseDTO role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role name cannot be empty." });

            var found = await _roleManager.FindByNameAsync(role.RoleName);
            if (found != null)
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });

            return await _roleManager.CreateAsync(new IdentityRole
            {
                Name = role.RoleName.Trim(),
                NormalizedName = role.RoleName.Trim().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }

        public async Task<IdentityResult> EditRoleAsync(UpdateRole role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role name cannot be empty." });

            var found = await _roleManager.FindByNameAsync(role.RoleName);
            if (found != null)
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });
            var updateRole = await _roleManager.FindByIdAsync(role.RoleId);

            return await _roleManager.UpdateAsync(updateRole);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Any())
                return IdentityResult.Failed(new IdentityError { Description = "Role is associated with users." });

            var result = await _roleManager.DeleteAsync(role);

            return result;
        }

        public async Task<List<IdentityRole>> GetallRoll()
        {
            var role = _roleManager.Roles.ToList();
            return role;
        }
    }
}

