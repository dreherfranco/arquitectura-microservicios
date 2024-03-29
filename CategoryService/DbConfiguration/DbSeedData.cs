using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.DbConfiguration
{
    public static class DbSeedData
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
           // if(isProd)
           // {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
          //  }
            
            if(!context.Categories.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Categories.AddRange(
                    new Category(){ Name="Microprocesadores" },
                    new Category(){ Name="RAMs" }                
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
