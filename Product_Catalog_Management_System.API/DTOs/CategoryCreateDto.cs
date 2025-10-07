using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
