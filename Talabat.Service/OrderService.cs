using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entity;
using Talabat.Core.Entity.basket;
using Talabat.Core.Entity.Order_Aggregrate;
using Talabat.Core.Repostiries_contract;
using Talabat.Core.ServiceContract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.OrderSpec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {

        //public OrderService(IBasketRepository basketRepository,IGenaricRepository<Product> prodGen,IGenaricRepository<DeliveryMethod> DelivryGen,IGenaricRepository<Order> OrderGen)
        //{
        //    _BasketRepository = basketRepository;
        //    _ProdGen = prodGen;
        //    _DelivryGen = DelivryGen;
        //    _OrderGen = OrderGen;
        //}
        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _UnitOfWork = unitOfWork;
            _BasketRepository = basketRepository;
        }

        public IBasketRepository _BasketRepository { get; }
        //public IGenaricRepository<Product> _ProdGen { get; }
        //public IGenaricRepository<DeliveryMethod> _DelivryGen { get; }
        //public IGenaricRepository<Order> _OrderGen { get; }
        public IUnitOfWork _UnitOfWork { get; }

        public async Task<Order?> CreateOrderAsync(string Buyer, string basketId, int deliveryMethodId, Address address)
        {
            //1 - get basket from Repo
            var basketitem = await _BasketRepository.GetBasketAsync(basketId);

            //2- Get Selected item at basket from product repo

            var Orderitems = new List<OrderItem>();

            if (basketitem?.BasketItem?.Count > 0)
            {
                foreach (var item in basketitem.BasketItem)
                {
                    var product = await _UnitOfWork.Repo<Product>().GetByID(item.Id);

                    var ProdOrderItem = new ProductOrderItem(item.Id, product.Name, product.PictureUrl);
                    var obj = new OrderItem(ProdOrderItem, product.price, item.Quantity);
                    Orderitems.Add(obj);
                }
            }

            //3-Get DelivryMethod from delivry repo

            var delivryItem = await _UnitOfWork.Repo<DeliveryMethod>().GetByID(deliveryMethodId);

            //4-create Order
            var order = new Order(Buyer, address, delivryItem, Orderitems);

            await _UnitOfWork.Repo<Order>().AddAsync(order);



            //5 - save change
            if (await _UnitOfWork.Commit() <= 0)
                return null;

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOderForUserAsync(string BuyerEmail)
        {
            ISpecification<Order> specification = new OrderSpecification(BuyerEmail);
            var orderRepo = await _UnitOfWork.Repo<Order>().GetAllSpec(specification);


            return (IReadOnlyList<Order>)orderRepo;
        }

        public async  Task<Order?> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            ISpecification<Order> specification = new OrderSpecification(BuyerEmail, OrderId);
            var orderRepo = await _UnitOfWork.Repo<Order>().GetByIDSpec(specification);


            return orderRepo is null ? null : orderRepo;
        }
    }
}
