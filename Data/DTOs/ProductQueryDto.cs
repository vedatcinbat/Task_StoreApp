namespace StoreApp.API.Data.DTOs
{
    public class ProductQueryDto
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set;}
        public string SortBy { get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }   
    }
}
