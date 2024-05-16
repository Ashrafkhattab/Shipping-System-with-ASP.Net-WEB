//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Shipping.Core.Model;
//using Shipping.Core.Repositries.contract;
//using Shipping.Repositry.Data;

//namespace Shipping.Repositry.Repositories
//{
//    public class GenricRepository<T> : IGenricRepository<T> where T : ModelBase
//    {
//        private readonly ShippingContext _context;

//        public GenricRepository(ShippingContext context)
//        {
//            _context = context;
//        }
//        public void Add(T model)
//        {
//            _context.Add(model);
//        }

//        public void Delete(int id)
//        {
//          var item =   GetById(id);
//            if (item != null)
//            {
//                item.
//            }
//        }

//        public List<T> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public T GetById(int id)
//        {
//            return _context.Find<T>( id);
//        }

//        public void SaveChanges()
//        {
//            _context.SaveChanges();
//        }

//        public void Update(T updateCity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
