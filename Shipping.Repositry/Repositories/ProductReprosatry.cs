using Microsoft.EntityFrameworkCore;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.Repositry.Data;

namespace Shipping.Repositry.Repositories
{ 
    public class ProductReprosatry :IProductRepeosatry
    {
        private readonly ShippingContext context;

        public ProductReprosatry(ShippingContext context)
        {
            this.context = context;
        }

        public bool AddRange(List<Product> products)
        {
            try
            {
                context.Products.AddRange(products);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteRange(List<Product> products)
        {
            try
            {
                context.Products.RemoveRange(products);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetProductsByOrderId(int id)
        {
            return context.Products.Where(d => d.isDeleted == false && d.OrderId == id).ToList();

        }
    }
}
