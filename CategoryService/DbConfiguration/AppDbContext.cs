using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.DbConfiguration
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var category1 = new Category(){
                Id=1, 
                Name="Microprocesadores"
            };
            var category2 = new Category(){
                Id=2, 
                Name="RAMs"
            };

            modelBuilder.Entity<Category>()
                .HasData(new List<Category>() { 
                    category1, category2               
            });

        }
    }
}