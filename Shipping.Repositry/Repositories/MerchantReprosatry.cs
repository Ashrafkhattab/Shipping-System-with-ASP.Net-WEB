using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Repositry.Data;
using System.Linq.Expressions;

namespace Shipping.Repositry.Repositories
{
    public class MerchantReprosatry : IMerchantReprosatry
    {
        private readonly UserManager<AppUser> user;
        private readonly ShippingContext context;

        public MerchantReprosatry(UserManager<AppUser> user, ShippingContext context)
        {
            this.user = user;
            this.context = context;
        }

        public async Task CreateAsync(Marchant merchant)
        {
            await context.Marchants.AddAsync(merchant);
        }

        public async Task<IEnumerable<Marchant>> GetAllMerchants()
        {
            return await context.Marchants
                .Include(x => x.AppUser)
                .ThenInclude(x => x.branch)
                .Include(x => x.City)
                .Include(x => x.Governorate).ToListAsync();
        }

        public async Task<Marchant?> GetMerchant(int id )
        {
            return context.Marchants.Include(x => x.SpecialPrices).Include(x => x.AppUser).FirstOrDefault(m=>m.Id == id && m.AppUser.IsDeleted== false);
        }
        public async Task<(int,string,string)> GetMerchantBystringId(string id)
        {
            //////return context.Marchants.Include(x => x.SpecialPrices).Include(x => x.AppUser).FirstOrDefault(m => m.AppUserId == id && m.AppUser.IsDeleted == false);



            var query = from merchant in context.Marchants
                        join user in user.Users on merchant.AppUserId equals user.Id into userJoin
                        from subUser in userJoin.DefaultIfEmpty()
                        where merchant.AppUserId == id
                        select new
                        {
                            merchant.Id,
                            subUser.PhoneNumber,
                            subUser.Address
                        };

            var result = await query.FirstOrDefaultAsync();

            return (result?.Id ?? 0, result?.PhoneNumber ?? "", result?.Address ?? "");
        }


        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();

        public async Task UpdateMerchant(Marchant merchant)
        {
            context.Marchants.Update(merchant);
            
        }
    }
}
