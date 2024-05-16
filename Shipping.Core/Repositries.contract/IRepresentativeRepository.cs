
using Shipping.Core.Model;
using Shipping.DTO;

namespace Shipping.Core.Repositries.contract
{
    public interface IRepresentativeRepository
    {
         void UpdatePassword(UpdatePasswordDtos passwordDTO);
         Task<int> UpdateRepresntative(Representative rep);
         Task<Representative> GetRepresentativeById(int id);
         Task<List<Representative>> GetAllRepresentatives();
         Task<int> DeleteRepresentative(int id);
         Task CreateAsync(Representative representative);
         Task<int> GetRepresentiveBystringId(string id);

         Task SaveChanges();


    }
}
