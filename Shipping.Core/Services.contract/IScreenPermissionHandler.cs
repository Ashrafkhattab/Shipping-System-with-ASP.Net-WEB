

using Shipping.DTO;

namespace Shipping.Core.Services.contract
{
    public interface IScreenPermissionHandler
    {
        Task<List<PermissionScreenDTO>> GetPermissions(string roleName);
        Task UpdatePermission(PermissionScreensRequestDTO permission);
    }
}
