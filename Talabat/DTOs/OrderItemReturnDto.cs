namespace Talabat.api.DTOs
{
    public class OrderItemReturnDto
    {

        public int productId { get; set; }
        public string productName { get; set; }
        public string productUrl { get; set; }
        public decimal Price { get; set; }
        public int Quntity { get; set; }
    }
}
