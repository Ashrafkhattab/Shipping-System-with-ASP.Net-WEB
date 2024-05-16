using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Model.OrderAggregate
{
    public enum Status
    {
        [EnumMember(Value = "New")]
        New,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Representitive Delivered")]
        RepresentitiveDelivered,
        [EnumMember(Value = "Client Delivered")]
        ClientDelivered,
        [EnumMember(Value = "UnReachable")]
        UnReachable,
        [EnumMember(Value = "Postponed")]
        Postponed,
        [EnumMember(Value = "Partially Delivered")]
        PartiallyDelivered,
        [EnumMember(Value = "Client Canceled")]
        ClientCanceled,
        [EnumMember(Value = "Reject With Paying")]
        RejectWithPaying,
        [EnumMember(Value = "Reject With Partial Paying")]
        RejectWithPartialPaying,
        [EnumMember(Value = "Reject From Employee")]
        RejectFromEmployee,
    }
}
