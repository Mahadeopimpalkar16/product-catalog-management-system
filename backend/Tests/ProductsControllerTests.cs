using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Controllers;
using ProductCatalog.API.Data;
using ProductCatalog.API.Profiles;
using ProductCatalog.API.Repositories;
using Xunit;

namespace ProductCatalog.API.Tests
{
    public class ProductsControllerTests
    {
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var ctx = new AppDbContext(options);
            ctx.Categories.Add(new ProductCatalog.API.Entities.Category { Id = 1, Name = "C" });
            ctx.Products.Add(new ProductCatalog.API.Entities.Product { Name = "P", Price = 1, CategoryId = 1 });
            ctx.SaveChanges();

            var productRepo = new ProductRepository(ctx);
            var categoryRepo = new CategoryRepository(ctx);
            var mapper = CreateMapper();
            var controller = new ProductsController(productRepo, categoryRepo, mapper);

            var result = await controller.GetAll(null, null);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
