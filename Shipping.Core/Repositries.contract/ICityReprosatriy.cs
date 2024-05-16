
using Shipping.Core.Model;

namespace Shipping.Core.Repositries.contract
{
    public interface ICityReprosatriy:IGenricRepository<City>
    {
     
         List<City> GettallShowDrop();
  
         City GetByName(string name);
      

    }
}
