using ProductCatalog.API.Entities;

namespace ProductCatalog.API.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync(int? categoryId = null, string? sortBy = null, bool asc = true, int page = 1, int pageSize = 10);
        Task<Product?> GetWithCategoryByIdAsync(int id);
    }
}
