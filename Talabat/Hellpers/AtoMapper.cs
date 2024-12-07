using AutoMapper;
using Talabat.api.DTOs;
using Talabat.Core.Entity;
using Talabat.Core.Entity.basket;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.api.Hellpers
{
    public class AtoMapper:Profile
    {
        //private readonly IConfiguration _con;

        //public AtoMapper() : this(new ConfigurationManager())
        //{

        //}
        public AtoMapper()
        {
            //IConfiguration con = new ConfigurationManager();

            //_con = con;
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(d=>d.productBrand,o=>o.MapFrom(s=>s.productBrand.Name))
                .ForMember(d => d.productCategory, o => o.MapFrom(s => s.productCategory.Name))
                //.ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{_con["BaseUrl"]}{s.PictureUrl}"))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PoductPictureUrlResolver>());

            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            CreateMap<AddressDto, Talabat.Core.Entity.Order_Aggregrate.Address>();

            CreateMap<Order, OrderReturnDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.DeliveryMethodId, o => o.MapFrom(s => s.DeliveryMethod.ID))
                .ForMember(d=>d.OrderItems,o=>o.MapFrom(s=>s.Items));

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(d => d.productId, o => o.MapFrom(s => s.ProductOrderItem.productId))
                .ForMember(d => d.productName, o => o.MapFrom(s => s.ProductOrderItem.prodName))
                .ForMember(d => d.productUrl, o => o.MapFrom(s => s.ProductOrderItem.prodUrl))
                .ForMember(d=>d.productUrl,o=>o.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<AddressCriteriaDto, Core.Entity.Identity.Address>();
        }
    }
}
