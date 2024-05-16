using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Repositry.Data;

namespace Shipping.Repositry.Repositories
{
    public class ScreenPermisssionReprosatary : IScreenPermisssionReprosatary
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShippingContext _context;

        public ScreenPermisssionReprosatary(RoleManager<IdentityRole> roleManager, ShippingContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> GetrollId(string roleName)
        {
            var result = await _roleManager.FindByNameAsync(roleName);
            return result.Id;
        }
        public async Task<List<Screen>> GetAllScreensWithPermissions()
        {
            return await _context.Screens.Include(x => x.ScreenPermission).ToListAsync();
        }

        public async Task<List<ScreenPermission>> GetScreenPermissions(string roleId)
        {
            return await _context.ScreenPermissions.Where(x => x.RoleId == roleId).ToListAsync();
        }

        public async Task AddScreenPermissions(IEnumerable<ScreenPermission> screenPermissions)
        {
            await _context.ScreenPermissions.AddRangeAsync(screenPermissions);
            await _context.SaveChangesAsync();
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }
    }
}
