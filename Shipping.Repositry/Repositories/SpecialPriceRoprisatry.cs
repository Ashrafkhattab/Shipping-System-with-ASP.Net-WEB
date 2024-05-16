
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Repositry.Data;

namespace Shipping.Repositry.Repositories
{
    public class SpecialPriceRoprisatry : ISpecialPriceRopresatry
    {
        private readonly ShippingContext context;

        public SpecialPriceRoprisatry(ShippingContext context)
        {
            this.context = context;
        }
        public async Task<int> AddRangeAsync(List<SpecialPrice> specialPrices)
        {
            if (specialPrices == null || specialPrices.Count == 0)
            {
                return 0;
            }

            await context.SpecialPrices.AddRangeAsync(specialPrices);
            return await context.SaveChangesAsync();
        }

        public Task<List<SpecialPrice>> GetAllAsync()
        {
            return context.SpecialPrices.ToListAsync();
        }

        public async Task<List<SpecialPrice>> GetSpecialPricesByMerchantId(int Id)
        {
            return  context.SpecialPrices.Where(s => s.MerchentId == Id).ToList();
        }

        public async Task<int> RemoveRangeAsync(List<SpecialPrice> specialPrices)
        {
            if (specialPrices == null || specialPrices.Count == 0)
            {
                return 0;
            }

            context.SpecialPrices.RemoveRange(specialPrices);
            return await context.SaveChangesAsync();
        }
    }
}
