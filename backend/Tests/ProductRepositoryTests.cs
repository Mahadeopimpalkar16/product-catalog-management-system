using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;
using Xunit;

namespace ProductCatalog.API.Tests
{
    public class ProductRepositoryTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var ctx = new AppDbContext(options);
            ctx.Categories.Add(new Category { Id = 1, Name = "Test" });
            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task AddAndGetProduct_WithCategory()
        {
            using var ctx = CreateContext();
            var repo = new ProductRepository(ctx);
            var prod = new Product { Name = "X", Price = 9.99M, CategoryId = 1 };
            var added = await repo.AddAsync(prod);
            var fetched = await repo.GetWithCategoryByIdAsync(added.Id);
            Assert.NotNull(fetched);
            Assert.Equal("X", fetched!.Name);
        }
    }
}
