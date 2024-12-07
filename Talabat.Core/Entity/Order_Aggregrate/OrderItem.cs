using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.Order_Aggregrate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductOrderItem productOrderItem, decimal price, int quntity)
        {
            ProductOrderItem = productOrderItem;
            Price = price;
            Quntity = quntity;
        }

        public ProductOrderItem ProductOrderItem {  get; set; }
        public decimal Price { get; set; }
        public int Quntity { get; set; }
    }
}
