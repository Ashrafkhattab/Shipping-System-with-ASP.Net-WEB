using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Model.OrderAggregate
{
    public enum TypeOfPaying
    {
        [EnumMember(Value = "Pre Delvery")]
        PreDelvery = 1,
        [EnumMember(Value = "After Delvery")]
        AfterDelvery = 2
    }
}
