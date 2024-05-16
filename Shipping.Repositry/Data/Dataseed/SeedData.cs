using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shipping.Core.Model;

namespace Shipping.Repositry.Data.Dataseed
{
    public static class SeedData
    {
        public  async static Task SeedRole(RoleManager<IdentityRole> _roleManger)
        {
            if (_roleManger.Roles.Count()==0)
            {
                await _roleManger.CreateAsync(new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),  
                });
            }
        }
        public async static Task SeedAdmin(UserManager<AppUser> _userManger)
        {
            if (_userManger.Users.Count()==0)
            {
                var User = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "admin".ToUpper(),
                    UserName = "admin".ToUpper(),
                    Email = "admin@admin.com",
                    BranchId = null,
                    IsDeleted = false
                };

                var result = await _userManger.CreateAsync(User, "admin@123A");
                await _userManger.AddToRoleAsync(User,"Admin");
            }
        }
    }
}
