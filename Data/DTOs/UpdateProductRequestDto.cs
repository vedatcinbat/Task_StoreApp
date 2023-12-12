namespace StoreApp.API.Data.DTOs
{
    public class UpdateProductRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? IsDeleted { get; set; }

        public int? CategoryId { get; set; }
    }
}
