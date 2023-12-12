using StoreApp.API.Data.Entities;

namespace StoreApp.API.Data.DTOs
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

       
    }
}
