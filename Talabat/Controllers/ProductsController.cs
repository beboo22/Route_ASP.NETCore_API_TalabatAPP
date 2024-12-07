using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Attributes;
using Talabat.api.DTOs;
using Talabat.api.Hellpers;
using Talabat.Core;
using Talabat.Core.Entity;
using Talabat.Core.Repostiries_contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpec;

namespace Talabat.api.Controllers
{
    
    public class ProductsController : BaseController
    {
        //private readonly IGenaricRepository<Product> genaricRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[HttpGet("GetAll/{sort}/{BrandId}/{CategoryId}")]
        [Cache(30)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagination<ProductToReturnDto>>>> GetAll([FromQuery]ProductCriteria spec )
        {
            ISpecification<Product> specification = new ProductwithSpec(spec);
            var genaricRepository = _unitOfWork.Repo<Product>();
            var items = await genaricRepository.GetAllSpec(specification);
            var countspec = new ProductCountSpecfication(spec);
            var count = await genaricRepository.CountSpec(countspec);
            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(items);
            return Ok(new Pagination<ProductToReturnDto>(spec.PadgeSize,spec.Padgenum,count,data));
        }

        [Cache(30)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            ISpecification<Product> specification = new ProductwithSpec(id);
            var genaricRepository = _unitOfWork.Repo<Product>();
            var prod = await genaricRepository.GetByIDSpec(specification);
            if(prod is null) 
                return NotFound();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prod));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Product>> Create(ProductCreationDto productCreationDto)
        {
            var MapedProduct = new Product()
            {
                Name = productCreationDto.Name,
                Description = productCreationDto.Description,
                PictureUrl = productCreationDto.PictureUrl,
                price = productCreationDto.price,
                BrandId = productCreationDto.BrandId is not null ? productCreationDto.BrandId.Value : null,
                CategoryId = productCreationDto.CategoryId is not null ? productCreationDto.CategoryId.Value : null,

            };

            var prodRepo = _unitOfWork.Repo<Product>();
            await prodRepo.AddAsync(MapedProduct);

            await _unitOfWork.Commit();
            //_unitOfWork.Dispose();

           return  await GetById(MapedProduct.ID);

        }



    }
}
