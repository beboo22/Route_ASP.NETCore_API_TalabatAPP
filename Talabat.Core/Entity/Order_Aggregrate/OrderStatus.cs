using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.Order_Aggregrate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pinding")]
        Pinding,
        [EnumMember(Value = "PaymentSucceeded")]
        PaymentSucceeded,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
    }
}
