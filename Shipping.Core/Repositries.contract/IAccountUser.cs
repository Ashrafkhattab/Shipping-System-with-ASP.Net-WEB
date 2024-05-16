using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;

namespace Shipping.Core.Repositries.contract
{
    public interface IAccountUser
    {
         Task<IdentityResult> CreateUser(AppUser appUser, string password);
         Task<AppUser?> GetUser(string username);
         Task<bool> GetPassword(AppUser appUser, string password);
         Task<List<AppUser>> GetAllEmployees();

    }
}
