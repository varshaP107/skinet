using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            //options is connection string
        }
        //specify DbSet in this way, allows us to query those entities and retrieve the data from our DB
        public DbSet<Product> Products { get; set; }   //Products will be the name of the table that gets created when we generate the code
            //DbSet<Product> will allow us to access the context and Products wil allow us to access the products and then use some of the methods that are defined inside the DbContext 
        
    }
}