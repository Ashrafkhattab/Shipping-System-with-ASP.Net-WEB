using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model.OrderAggregate;
using Shipping.Core.Repositries.contract;
using Shipping.MiddlWares;
using Shipping.Repositry.Data;

namespace Shipping.Repositry.Repositories
{
    public class OrderReposiatry : IOrderReposiatry
    {
        private readonly ShippingContext context;

        public OrderReposiatry(ShippingContext context) 
        {
            this.context = context;
        }


        public bool AddOrder(Order order)
        {
            try
            {
                context.Order.Add(order);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public  async Task<double> GetCostByShippingTypeAsync(DeliveryMethode shippingType)
        {
            
            switch (shippingType)
            {
                case shippingType.Id = 1:return 0;
                case TypeOfDelevery.FastDelvery: return 300;
                case TypeOfDelevery.DelveryOfSomeDays: return 100;
                default: break;
            }

            throw new ExceptionLogic($"Shipping type '{shippingType}' not found.");
        }

        public  Order GetOrderById(int orderId)
        {
            try
            {
                var order =  context.Order
                    .Include(gov => gov.Governorate)
                    .Include(city => city.City)
                    .Include(product => product.Products)
                    .Include(branch => branch.Branches)
                    .Where(o => o.Id == orderId).FirstOrDefault();
                    //.Include(ship=> ship.typeOfDelevery)
               return order;
            }
            catch (Exception)
            {
                return null!;
            }

        }

        public bool RemoveOrder(Order order)
        {
            try
            {
                context.Order.Remove(order);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool SaveChanges()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool UpdateOrder(Order order)
        {
            try
            {
                context.Order.Update(order);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //report
        public int CountAll()
        {
            return context.Order
                .Where(d => d.isDeleted == false)
                .Count();
        }

        public int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, Status status)
        {
            toDate = toDate.AddDays(1);
            return context.Order
               .Where(d => d.isDeleted == false && d.Date > fromDate && d.Date < toDate && d.status == status)
               .Count();
        }
        
        public IEnumerable<Order> GetAll(int pageNumer, int pageSize)
        {
            return context.Order
                            .Where(d => d.isDeleted == false)
                            .Skip((pageNumer - 1) * pageSize)
                            .Take(pageSize)
                            .Include(gover => gover.Governorate)
                            .Include(city => city.City)
                            .Include(merchant => merchant.Merchant.AppUser)
                            
                            .AsNoTracking();
        }
        public  IEnumerable<Order> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, Status status)
        {
            toDate = toDate.AddDays(1);            
            var result =  context.Order
               .Where(d => d.isDeleted == false && d.Date > fromDate && d.Date < toDate && d.status == status)
               .Skip((pageNumer - 1) * pageSize)
               .Take(pageSize)
               .Include(gover => gover.Governorate)
               .Include(city => city.City)
               .Include(merchant => merchant.Merchant.AppUser)
               .AsNoTracking();
            return result;
        }
        //home
        public List<int> CountOrdersForEmployeeByStatus()
        {
            return context.Order.Where(s => s.isDeleted == false).Select(s => (int)s.status).ToList();
        }
        public List<int> CountOrdersForMerchantByStatus(int merchantId)
        {
            return context.Order.Where(s => s.isDeleted == false && s.MerchantId == merchantId).Select(s => (int)s.status).ToList();
        }
        public List<int> CountOrdersForRepresentativeByStatus(int representativeId)
        {
            return context.Order.Where(s => s.isDeleted == false && s.RepresentativeId == representativeId).Select(s => (int)s.status).ToList();
        }

        //show orders
        public IEnumerable<Order> GetOrdersForEmployee(string searchText, int statusId, int pageNumer, int pageSize)
        {
            return context.Order
                .Where(o => o.status == (Status)statusId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }
        public IEnumerable<Order> GetOrdersForMerchant(string searchText, int merchantId, int statusId, int pageNumer, int pageSize)
        {
            return context.Order
                .Where(o => o.status == (Status)statusId && o.MerchantId == merchantId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }
        public IEnumerable<Order> GetOrdersForRepresentative(int representativeId, int statusId, int pageNumer, int pageSize, string searchText)
        {
            return context.Order
                .Where(o => o.status == (Status)statusId && o.isDeleted == false && o.RepresentativeId == representativeId && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }
        //count
        public int GetCountOrdersForEmployee(int statusId, string searchText)
        {
            return context.Order
                .Where(o => o.status == (Status)statusId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Count();
        }
        public int GetCountOrdersForMerchant(int merchantId, int statusId, string searchText)
        {
            return context.Order
                .Where(o => o.status == (Status)statusId && o.MerchantId == merchantId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Count();
        }
        public int GetCountOrdersForRepresentative(int representativeId, int statusId, string searchText)
        {
            return context.Order
               .Where(o => o.status == (Status)statusId && o.isDeleted == false && o.RepresentativeId == representativeId && o.ClientName.StartsWith(searchText))
               .Count();
        }

    }
}
