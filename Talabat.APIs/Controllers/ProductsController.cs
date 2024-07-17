using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecsParams productSpecs)
        {
            var sepcs = new ProductWithNavigationsSpecifications(productSpecs);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(sepcs);
            var ProductsDto = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            var countSpecs = new ProductsCountSpecification(productSpecs);
            var countOfProducts = await _unitOfWork.Repository<Product>().GetCount(countSpecs);
            return Ok(new Pagination<ProductToReturnDto>(productSpecs.PageIndex, productSpecs.PageSize, countOfProducts, ProductsDto));
        }



        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var specs = new ProductWithNavigationsSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(specs);
            var productToDto = _mapper.Map<ProductToReturnDto>(product);

            if (productToDto is null)
                return NotFound(new ApiResponse(404));

            return Ok(productToDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return Ok(brands);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

            return Ok(categories);
        }



    }
}
