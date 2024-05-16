using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Model.OrderAggregate
{
    public enum OrderType
    {
        [EnumMember(Value = "Receive From The Branch")]
        ReceiveFromTheBranch = 1,
        [EnumMember(Value = "Receive From The Trader")]
        ReceiveFromTheTrader = 2

    }
}
