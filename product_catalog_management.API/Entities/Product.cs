using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
