using StoreApp.API.Data.Entities;

namespace StoreApp.API.Data.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public List<CategoryProductDto?> Products { get; set; }

        public double CategoryTotalPrice => Products?.Sum(p => p.Price * p.Quantity) ?? 0;

        public int CategoryTotalCount => Products?.Sum(p => p.Quantity) ?? 0;

        
    }
}
