using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id); //Async at the end is just a naming convention so that we can easily identify that this is gona return a task and we can await this method when we call it in controller.
        Task<IReadOnlyList<Product>> GetProductsAsync();  //IReadOnlyList means we cant add or reemove products from it.While returning we can be more specific, what we r going to return.
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}