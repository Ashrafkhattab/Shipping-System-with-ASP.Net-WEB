using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Core.Model;

namespace Shipping.Core.Repositries.contract
{
    public interface IGenricRepository<T>where T : ModelBase
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T model);
        void Update(T updateModel);
        void Delete(int id);
        void SaveChanges();
    }
}
