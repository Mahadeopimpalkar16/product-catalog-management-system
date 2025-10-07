using ProductCatalog.API.Entities;


namespace ProductCatalog.API.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync(int? categoryId = null, string? sortBy = null, bool asc = true);
        Task<Product?> GetWithCategoryByIdAsync(int id);
    }

}