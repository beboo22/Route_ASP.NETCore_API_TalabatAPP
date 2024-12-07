using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.DTOs;
using Talabat.Core.Entity;
using Talabat.Core.Repostiries_contract;
using Talabat.Core.Specification.ProductSpec;
using Talabat.Core.Specification;
using Talabat.api.Errors;
using Talabat.Repository.Data;
using Talabat.Core;

namespace Talabat.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        //private readonly IGenaricRepository<Product> genaricRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private StoreDbContext db;


        public BugController(IUnitOfWork unitOfWork, IMapper mapper, StoreDbContext _db)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.db = _db;
        }

      

        [HttpGet("notFound/{id}")]
        public  async Task<IActionResult> notfound(int id)
        {
            ISpecification<Product> specification = new ProductwithSpec(id);
            var genaricRepository = _unitOfWork.Repo<Product>();
            var prod = await genaricRepository.GetByIDSpec(specification);
            if (prod is null)
                return NotFound();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prod));

        }
        
        [HttpGet("notfoundHandle/{id}")]
        public  async Task<IActionResult> notfoundHandle(int id)
        {
            ISpecification<Product> specification = new ProductwithSpec(id);
            var genaricRepository = _unitOfWork.Repo<Product>();

            var prod = await genaricRepository.GetByIDSpec(specification);
            if (prod is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prod));

        }

        [HttpGet("BadReqVAlidation/{id}")] // BadReq/one
        public  async Task<IActionResult> BadReqForVAlidation(int id) // validationerror
        {
            ISpecification<Product> specification = new ProductwithSpec(id);
            var genaricRepository = _unitOfWork.Repo<Product>();

            var prod = await genaricRepository.GetByIDSpec(specification);
            if (prod is null)
                return NotFound();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prod));

        }
        
        [HttpGet("BadReqVAlidationHandle/{id}")] // BadReq/one
        public  async Task<IActionResult> BadReqForVAlidationHandle(int id) // validationerror
        {
            ISpecification<Product> specification = new ProductwithSpec(id);
            var genaricRepository = _unitOfWork.Repo<Product>();

            var prod = await genaricRepository.GetByIDSpec(specification);
            if (prod is null)
                return NotFound();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(prod));

        }

        [HttpGet("BadReqHandle/{id}")]
        public IActionResult BadReqHandle()
        { return BadRequest(new ApiResponse(400)); }
        
        [HttpGet("BadReq/{id}")]
        public IActionResult BadReq()
        { return BadRequest(); }


        [HttpGet("ExceptionMidleware/")]
        public async Task<IActionResult> ExceptionMidleware()
        {
            //ISpecification<Product> specification = new ProductwithSpec(100);
            var prod = db.Set<Product>().Find(100);
            var prodToRet = prod.ToString();
            return Ok(prodToRet);

        }



    }
}
