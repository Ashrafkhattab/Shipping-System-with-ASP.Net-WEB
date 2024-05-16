using Shipping.Core.Model;
using System.Linq.Expressions;

namespace Shipping.Core.Repositries.contract
{
    public interface IMerchantReprosatry 
    {
         Task<Marchant?> GetMerchant(int id );
         Task<(int, string, string)> GetMerchantBystringId(string id );
         Task UpdateMerchant(Marchant merchant);
         Task<IEnumerable<Marchant>> GetAllMerchants();

        Task CreateAsync(Marchant merchant);
        Task<int> SaveChangesAsync();
    }
}
