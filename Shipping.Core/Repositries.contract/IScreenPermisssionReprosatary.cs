using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;

namespace Shipping.Core.Repositries.contract
{
    public interface IScreenPermisssionReprosatary
    {
          Task<bool> RoleExists(string roleName);

          Task<List<Screen>> GetAllScreensWithPermissions();

          Task<List<ScreenPermission>> GetScreenPermissions(string roleId);
          Task<string> GetrollId(string roleName);

          Task AddScreenPermissions(IEnumerable<ScreenPermission> screenPermissions);
         void Savechanges();

    }
}
