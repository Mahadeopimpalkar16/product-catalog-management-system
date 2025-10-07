using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalog.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbOptions;

        public ProductRepositoryTests()
        {
            // In-memory database for testing
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private async Task SeedDatabase(AppDbContext context)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "iPhone", Price = 1000, CategoryId = 1 },
                new Product { Id = 2, Name = "Samsung TV", Price = 800, CategoryId = 1 },
                new Product { Id = 3, Name = "C# Book", Price = 50, CategoryId = 2 }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllWithCategoryAsync_ReturnsAllProducts()
        {
            // Arrange
            using var context = new AppDbContext(_dbOptions);
            await SeedDatabase(context);

            var repository = new ProductRepository(context);

            // Act
            var products = await repository.GetAllWithCategoryAsync();

            // Assert
            Assert.Equal(3, products.Count());
            Assert.All(products, p => Assert.NotNull(p.Category));
        }

        [Fact]
        public async Task GetAllWithCategoryAsync_FiltersByCategory()
        {
            using var context = new AppDbContext(_dbOptions);
            await SeedDatabase(context);

            var repository = new ProductRepository(context);

            // Act
            var products = await repository.GetAllWithCategoryAsync(categoryId: 1);

            // Assert
            Assert.Equal(2, products.Count());
            Assert.All(products, p => Assert.Equal(1, p.CategoryId));
        }

        [Fact]
        public async Task GetAllWithCategoryAsync_SortsByPriceDescending()
        {
            using var context = new AppDbContext(_dbOptions);
            await SeedDatabase(context);

            var repository = new ProductRepository(context);

            // Act
            var products = await repository.GetAllWithCategoryAsync(sortBy: "price", asc: false);

            // Assert
            Assert.Equal(1000, products.First().Price);
            Assert.Equal(50, products.Last().Price);
        }

        [Fact]
        public async Task GetWithCategoryByIdAsync_ReturnsCorrectProduct()
        {
            using var context = new AppDbContext(_dbOptions);
            await SeedDatabase(context);

            var repository = new ProductRepository(context);

            // Act
            var product = await repository.GetWithCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("iPhone", product!.Name);
            Assert.NotNull(product.Category);
        }

        [Fact]
        public async Task GetWithCategoryByIdAsync_ReturnsNullForInvalidId()
        {
            using var context = new AppDbContext(_dbOptions);
            await SeedDatabase(context);

            var repository = new ProductRepository(context);

            // Act
            var product = await repository.GetWithCategoryByIdAsync(999);

            // Assert
            Assert.Null(product);
        }
    }
}
