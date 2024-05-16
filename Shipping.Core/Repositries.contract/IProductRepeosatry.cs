using Shipping.Core.Model;

namespace Shipping.Core.Repositries.contract
{
    public interface IProductRepeosatry
    {
        bool AddRange(List<Model.Product> products);
        bool DeleteRange(List<Model.Product> products);
        List<Model.Product> GetProductsByOrderId(int id);
    }
}
