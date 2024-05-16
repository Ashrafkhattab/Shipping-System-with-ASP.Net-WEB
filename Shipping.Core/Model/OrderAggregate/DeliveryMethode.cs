using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Model.OrderAggregate
{
    //public enum TypeOfDelevery
    //{
    //    NormalDelvery = 1,
    //    FastDelvery = 2,
    //    DelveryOfSomeDays = 3,
    //}
    public class DeliveryMethode : ModelBase
    {
        public DeliveryMethode()
        {
            
        }
        public DeliveryMethode(string delveryTime, string description, decimal cost)
        {
            DelveryTime = delveryTime;
            Description = description;
            Cost = cost;
        }

        public string DelveryTime { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

    }
}
