using ProductCatalog.API.Data;
using ProductCatalog.API.Entities;

namespace ProductCatalog.API.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
