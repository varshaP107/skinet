using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    //api/ is optional but its conventional
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        //endpoints or methods
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()  //here we're creating a task thats going to pass off our request to a delegate. and not going to wait and block the thread that this request is running on until that task is completed.
        {
            var products = await _context.Products.ToListAsync(); //ToList() method is from Linq. when we execute ToList() then it execute a select query on our db and return the result. Here we will use Async version of this method.
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}