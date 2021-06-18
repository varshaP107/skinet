using System.Reflection;
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
        public DbSet<ProductType> ProductTypes { get; set;}
        public DbSet<ProductBrand> ProductBrands { get; set;} //so that we can query these tables as well

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //when we create our migration, this is the method responsible for creating migrations. So we override this method and tell it to look for our config.
        {
            base.OnModelCreating(modelBuilder);    //base is the class we r deriving from DbContext class
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}