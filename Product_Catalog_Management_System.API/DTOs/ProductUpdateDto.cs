using System.ComponentModel.DataAnnotations;


namespace ProductCatalog.API.DTOs
{
    public class ProductUpdateDto
    {
        [Required] 
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}