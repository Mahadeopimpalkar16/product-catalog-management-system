using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Entities;

namespace ProductCatalog.API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync(int? categoryId = null, string? sortBy = null, bool asc = true, int page = 1, int pageSize = 10)
        {
            var query = _dbSet.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "name")
                    query = asc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                else if (sortBy.ToLower() == "price")
                    query = asc ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            var skip = (page - 1) * pageSize;
            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetWithCategoryByIdAsync(int id)
        {
            return await _dbSet.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
