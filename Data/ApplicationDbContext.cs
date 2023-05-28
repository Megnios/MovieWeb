﻿using Microsoft.EntityFrameworkCore;
using MovieWeb.Areas.Admin.Models;
using MovieWeb.Models;

namespace MovieWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "SciFi", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Action", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Jurassic Park",
                    Rating = 8.2,
                    Director = "Steven Spielberg",
                    ListPrice = 20.33

                },
                new Product
                {
                    Id = 2,
                    Name = "Stars Wars",
                    Rating = 8.6,
                    Director = "George Lucas",
                    ListPrice = 25.99
                },
                new Product
                {
                    Id = 3,
                    Name = "The Simpsons",
                    Rating = 7.3,
                    Director = "David Silverman",
                    ListPrice = 10.5
                }
                );
        }
    }
}
