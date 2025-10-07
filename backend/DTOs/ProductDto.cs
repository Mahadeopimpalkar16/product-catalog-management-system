namespace ProductCatalog.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
