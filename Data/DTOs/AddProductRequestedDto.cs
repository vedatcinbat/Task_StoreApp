using System.ComponentModel.DataAnnotations;

namespace StoreApp.API.Data.DTOs
{
    public class AddProductRequestedDto
    {
        
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }

    }
}
