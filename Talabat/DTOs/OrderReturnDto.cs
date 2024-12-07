using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.api.DTOs
{
    public class OrderReturnDto
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }

        public string OrderStatus { get; set; }
        public Address Address { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemReturnDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
