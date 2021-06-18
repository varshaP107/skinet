using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    //api/ is optional but its conventional
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        //endpoints or methods
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()  //here we're creating a task thats going to pass off our request to a delegate. and not going to wait and block the thread that this request is running on until that task is completed.
        {
            var products = await _repo.GetProductsAsync();  //ToList() method is from Linq. when we execute ToList() then it execute a select query on our db and return the result. Here we will use Async version of this method.
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync()); //we cant explicitly convert IReadonlyList ProductBrands to ActionResult IreadonlyList ProductBrand . Its simply doesnt allow us to return IReadOnlyList directly, so wrap it into OK Response
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync()); 
        }
    }
}