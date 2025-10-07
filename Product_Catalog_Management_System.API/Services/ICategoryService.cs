using ProductCatalog.API.DTOs;

namespace ProductCatalog.API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
