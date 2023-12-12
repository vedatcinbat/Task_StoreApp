namespace StoreApp.API.Data.DTOs
{
    public class ProductFilterSortPaginationDto
    {
        public string? search { get; set; }
        // Filter
        public int? Id { get; set; }
        public string? Description { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public string? CategoryName { get; set; }
        // Sort
        public string? sortby { get; set; }
        public string? sortorder { get; set; }
        // Pagination
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
