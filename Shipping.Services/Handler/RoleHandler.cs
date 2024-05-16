using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Core.Services.contract;
using Shipping.DTO;

namespace Shapping.Handler
{
    public class RoleHandler :IRoleHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleRopersiatry _roleRepository;

        public RoleHandler(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IRoleRopersiatry roleRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }
        
        public async Task<IdentityResult> AddRoleAsync(RoleBaseDTO role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new ArgumentException("Role name cannot be empty.");

            var result = await _roleRepository.AddRoleAsync(role);

            if (!result.Succeeded)
                throw new InvalidOperationException(GetErrorsDescription(result.Errors));

            return result;
        }


        public async Task<IdentityResult> EditRoleAsync(UpdateRole role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role name cannot be empty." });

            var result = await _roleRepository.EditRoleAsync(role);
            if (result != null)
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });
            var updateRole = await _roleManager.FindByIdAsync(role.RoleId);

            return result;
        }

        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var result = await _roleRepository.DeleteRoleAsync(id);

            if (!result.Succeeded)
                throw new InvalidOperationException(GetErrorsDescription(result.Errors));

            return result;
        }

       public string GetErrorsDescription(IEnumerable<IdentityError> errors)
       {
            var error = string.Empty;
            foreach (var item in errors)
            {
                error += item.Description + ", ";
            }
            return error;
       }

        public Task<List<IdentityRole>> GetAllRole()
        {
            return _roleRepository.GetallRoll();
        }
    }
}

