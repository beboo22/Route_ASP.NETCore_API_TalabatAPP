using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.Order_Aggregrate
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail/*, DateTimeOffset dateTimeOffset, OrderStatus status*/, Address address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            //DateTimeOffset = dateTimeOffset;
            //Status = status;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; }
        public Address Address { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal getTotal() => SubTotal + DeliveryMethod.Cost;

    }
}
