using ProductCatalog.API.DTOs;

namespace ProductCatalog.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? categoryId = null, string? sortBy = null, bool asc = true);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(ProductUpdateDto dto);
        Task DeleteProductAsync(int id);
    }
}
