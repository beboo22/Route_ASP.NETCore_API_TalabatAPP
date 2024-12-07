using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.Order_Aggregrate
{
    public class ProductOrderItem
    {
        public ProductOrderItem()
        {
        }

        public ProductOrderItem(int prodId, string prodName, string prodUrl)
        {
            this.productId = prodId;
            this.prodName = prodName;
            this.prodUrl = prodUrl;
        }

        public int productId { get; set; }
        public string prodName { get; set; }
        public string prodUrl { get; set; }

    }
}
