using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.Core.Specification.OrderSpec
{
    public class OrderSpecification:Specification<Order>
    {
        public OrderSpecification(string buyerEmil):base(O=>O.BuyerEmail == buyerEmil) 
        {
            AddIncludes();
        }
        public OrderSpecification(string buyerEmil,int orderId):base(O=>O.BuyerEmail == buyerEmil && O.ID == orderId) 
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(o=>o.Address);
            includes.Add(o=>o.DeliveryMethod);
            includes.Add(o=>o.Items);
        }
    }
}
