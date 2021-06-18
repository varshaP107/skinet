using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository  //click on IProductRepository and quick fix(ctrl+.) then choose implement interface
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
              .Include(p => p.ProductType)
              .Include(p => p.ProductBrand)
              .FirstOrDefaultAsync(p => p.Id ==id);   //error here becoz FindAsync(id) doesnt accept IQueryable(such type of query, which wr building here). So we can use here FirstOrDefaultAsync or SingleOrDefaultAsync()
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
               .Include(p => p.ProductType)
               .Include(p => p.ProductBrand)   //before we execute ToListAsync, we r building up query that we want Sql to accept before it returns a List
               .ToListAsync(); //this is the point when r query is send to Sql. our query is executed and we get the data back. So wr gona tell EF to include Product types and brands
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}