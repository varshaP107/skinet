using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            //wr going to run seed method from inside our Program class. So No Global exception handling available. so use try catch
            try{
                if(!context.ProductBrands.Any())  //check if we have any ProductBrands in our DB. If no, then seed
                {
                    var brandsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");  //specify path where our data is. So this method is going to run inside our Program class which is in API project So we will go up a level and then in Infrastructure proj. ->Data->SeedData->brands.json  
                    //now serialize into our products brand obj.
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);  //once r Json is deserialized into a list of our Products, we can add them to our DB via context

                    foreach(var item in brands)
                    {
                        context.ProductBrands.Add(item);  //context is going to tack everything that we add into product brands
                    }
                    await context.SaveChangesAsync(); //save all of our new product brands into DB
                }

                //same thing for Product type
                if(!context.ProductTypes.Any())
                {
                    var typesData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json"); 
                        
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    
                    foreach(var item in types)
                    {
                        context.ProductTypes.Add(item);  
                    }
                    await context.SaveChangesAsync(); 
                }

                //same thing for Product
                if(!context.Products.Any()) 
                {
                    var productsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json"); 
                   
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach(var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>(); //logging against StoreContextSeed class
                logger.LogError(ex.Message);
            }
        }
    }
}