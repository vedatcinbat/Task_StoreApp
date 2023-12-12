﻿namespace StoreApp.API.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public int? CategoryId { get; set; }


        // Navigations
        public Category? Category { get; set; }

    }
}
