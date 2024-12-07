using AutoMapper;
using Talabat.api.DTOs;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.api.Hellpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemReturnDto, string>
    {
        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _Config = configuration;
        }

        public IConfiguration _Config { get; }

        public string Resolve(OrderItem source, OrderItemReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ProductOrderItem.prodUrl))
                return $"{_Config["BaseUrl"]}{source.ProductOrderItem.prodUrl}";
            return string.Empty;
        }
    }
}
