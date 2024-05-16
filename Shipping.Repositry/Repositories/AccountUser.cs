using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;


namespace Shipping.Repository.Repositories
{
    public class AccountUser : IAccountUser
    {
        private readonly UserManager<AppUser> user;

        public AccountUser(UserManager<AppUser> user)
        {
            this.user = user;
        }
        public async Task <IdentityResult> CreateUser(AppUser appUser, string password)
        {
            return await user.CreateAsync(appUser, password);
        }

        public async Task<bool> GetPassword(AppUser appUser, string password)
        {
            return await user.CheckPasswordAsync(appUser, password);
        }

        public async Task<AppUser?> GetUser(string username)
        {
            return await user.FindByNameAsync(username);
        }

        public async Task<List<AppUser>> GetAllEmployees()
        {
            var employees = await user.GetUsersInRoleAsync("Employee");
            if (employees == null)
            {
                throw new ExceptionLogic ("no Employees");
            }
            return employees.ToList();
        }


    }
}
