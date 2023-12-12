using Microsoft.EntityFrameworkCore;
using StoreApp.API.Data.DTOs;
using StoreApp.API.Data.Entities;

namespace StoreApp.API.Data
{
    public class StoreAppDbContext : DbContext
    {
        public StoreAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        // Tables

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var testCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Description = "This is electronics category"},
                new Category { Id = 2, Name = "Books", Description = "This is books category"},
                new Category { Id = 3, Name = "Foods And Drinks", Description = "This is food and drinks category"},
                new Category { Id = 4, Name = "Clothes", Description = "This is clothes category"},
            };

            var testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Gaming Laptop", Description = "This has RTX-4090...", Price = 2000.25, Quantity = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "PS5", Description = "This is ps5...", Price = 1000, Quantity = 15, CategoryId = 1 },
                new Product { Id = 3, Name = "Lord Of The Rings - Two Tower", Description = "This has lotr...", Price = 24, Quantity = 20, CategoryId = 2 },
                new Product { Id = 4, Name = "Water - 5lt", Description = "This is water", Price = 5, Quantity = 100, CategoryId = 3 },
                new Product { Id = 5, Name = "Black Men Hoodie", Description = "This is hoodie for men", Price = 64, Quantity = 40, CategoryId = 4 },
                new Product { Id = 6, Name = "Men Parfume - BrandName", Description = "This is parfume for men", Price = 39.99, Quantity = 60, CategoryId = 4 },
                new Product { Id = 7, Name = "IPhone 15 Pro Max - 1TB", Description = "This is latest iphone", Price = 1299.99, Quantity = 10, CategoryId = 1 },
                new Product { Id = 8, Name = "Gaming Pc - 4090 | i9 | 16gbram", Description = "This is high-performance gaming pc", Price = 2900, Quantity = 15, CategoryId = 1 },
                new Product { Id = 9, Name = "Men Nike Air Max", Description = "This is sport men shoes", Price = 150, Quantity = 100, CategoryId = 4 },
                new Product { Id = 10, Name = "Logitech Gaming Keyboard", Description = "This is gaming keyboard", Price = 75, Quantity = 25, CategoryId = 1 }
            };

            modelBuilder.Entity<Category>().HasData(testCategories);
            modelBuilder.Entity<Product>().HasData(testProducts);

            modelBuilder.Entity<Product>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Category>().Property<bool>("IsDeleted");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            

        }
        



    }
}
