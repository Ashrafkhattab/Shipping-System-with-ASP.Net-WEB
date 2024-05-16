using System.ComponentModel.DataAnnotations.Schema;
using Shipping.Core.Model.OrderAggregate;

namespace Shipping.Core.Model
{
    public class Product : ModelBase
    {
         
        public string? Name { get; set; }
        public decimal Weigth { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("order")]
        public int? OrderId { get; set; }
        public virtual Order? order { get; set; }
    }
}
