using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }
    }
}
