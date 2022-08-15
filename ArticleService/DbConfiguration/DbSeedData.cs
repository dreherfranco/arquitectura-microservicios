using ArticleService.Models;
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
            
            if(!context.Articles.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Articles.AddRange(
                    new Article(){ Id=1, Name="articulo 1" },
                    new Article(){ Id=2, Name="articulo 2" }                
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