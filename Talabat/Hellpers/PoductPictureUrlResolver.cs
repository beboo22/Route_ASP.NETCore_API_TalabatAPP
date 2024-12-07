using AutoMapper;
using AutoMapper.Execution;
using Talabat.api.DTOs;
using Talabat.Core.Entity;

namespace Talabat.api.Hellpers
{
    public class PoductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _con;
        public PoductPictureUrlResolver(IConfiguration con)
        {
            _con = con;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_con["BaseUrl"]}{source.PictureUrl}";
            return string.Empty ;
        }
    }
}
