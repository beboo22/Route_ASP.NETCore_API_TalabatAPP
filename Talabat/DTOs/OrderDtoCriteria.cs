using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.api.DTOs
{
    public class OrderDtoCriteria
    {
        public string BuyerEmail { get; set; }
        public string basketId { get; set; }
        public AddressDto Address { get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
