using StoreApp.API.Data.Entities;

namespace StoreApp.API.Data.DTOs
{
    public class AddCategoryRequestedDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Product?> Products = new List<Product?>();
    }
}
