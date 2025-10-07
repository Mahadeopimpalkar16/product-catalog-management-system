using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
