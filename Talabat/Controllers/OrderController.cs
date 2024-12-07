using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Attributes;

//using StackExchange.Redis;
using Talabat.api.DTOs;
using Talabat.api.Errors;
using Talabat.Core.Entity.Order_Aggregrate;
using Talabat.Core.ServiceContract;

namespace Talabat.api.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IOrderService Order,IMapper mapper)
        {
            _Order = Order;
            _Mapper = mapper;
        }

        public IOrderService _Order { get; }
        public IMapper _Mapper { get; }

        [HttpPost]
        public async Task<ActionResult<OrderReturnDto>> CreateOrder(OrderDtoCriteria orderDto)
        {
            //var DelivryMapping = _Mapper.Map<DeliveryMethod,DeliveryMethodDto>(orderDto.DeliveryMethod);

            var ShippingAddress = _Mapper.Map<AddressDto, Address>(orderDto.Address);
            

            var item = await _Order.CreateOrderAsync(orderDto.BuyerEmail, orderDto.basketId, orderDto.DeliveryMethodId, ShippingAddress);



            if (item is null) return BadRequest(new ApiResponse(400));

            var MapOrder = _Mapper.Map<OrderReturnDto>(item);
            return MapOrder;





        }

        [Cache(30)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDto>>> GetOrdersForUser (string BuyerEmail)
        {
            var item = await _Order.GetOderForUserAsync(BuyerEmail);

            if (item.Count() == 0) return BadRequest(new ApiResponse(400));
            var MapOrder = _Mapper.Map<IReadOnlyList<OrderReturnDto>>(item);
            //return MapOrder;
            return Ok(MapOrder);
        }

        [Cache(30)]
        [HttpGet("{orderid}")]
        public async Task<ActionResult<OrderReturnDto>> GetOrdersForUser (int orderid,string BuyerEmail)
        {
            var item = await _Order.GetOrderByIdForUserAsync(orderid,BuyerEmail);

            if (item is null ) return BadRequest(new ApiResponse(400));
            var MapOrder = _Mapper.Map<OrderReturnDto>(item);
            //return MapOrder;
            return Ok(MapOrder);
            //return Ok(item);
        }



    }
}
