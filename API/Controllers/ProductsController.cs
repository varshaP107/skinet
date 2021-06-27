using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    //api/ is optional but its conventional
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
             IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo,
             IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
        }

        //endpoints or methods
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()  //here we're creating a task thats going to pass off our request to a delegate. and not going to wait and block the thread that this request is running on until that task is completed.
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec); //Hitting DB here.ToList() method is from Linq. when we execute ToList() then it execute a select query on our db and return the result. Here we will use Async version of this method. ListAsync() takes a specifications
            
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products)); //here we shapping data in memory. not hitting db
            
            //we need to wrap it inside ok obj becuase we r returning readonly List from Repository
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _productsRepo.GetEntityWithSpec(spec);
           
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync()); //we cant explicitly convert IReadonlyList ProductBrands to ActionResult IreadonlyList ProductBrand . Its simply doesnt allow us to return IReadOnlyList directly, so wrap it into OK Response
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}