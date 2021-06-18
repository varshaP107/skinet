using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //we r outside of our services container in the startup class, So here we dont have control over the lifetime of when we create this particular instance of our context. 
            //So wr gona do it inside using stmnt. any code that run inside using stmnt is gona be disposed off as soon as we've finished with the methods inside that. we dont need to worry about cleaning.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;  //wr gona get our services
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();//we can log any info out into our console. we will create an instance of ILoggerFactory. a LoggerFactory allows us to create an instance of Logger class.
                //we r outside of startup class so we dont have exception handling here. So we need to do it by ourself
                try{
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync(); //MigrationAsync() method applies any pending migrations for the context to a DB and it will create the DB if it does not already exist.
                    
                    await StoreContextSeed.SeedAsync(context, loggerFactory);  //to seed the data into DB tables from json files which we copied in SeedData Folder
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>(); //Program is the that we needs to log against
                    logger.LogError(ex, "An error occurs during migration");
                }
            }
            host.Run(); //to start our appl we need to call run method. by default it was attached above
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
