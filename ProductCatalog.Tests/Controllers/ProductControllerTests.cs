using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.API.Controllers;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalog.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithProductList()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            var sampleProducts = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Test Product", Price = 100, CategoryName = "Electronics" }
            };

            mockService.Setup(s => s.GetAllProductsAsync(null, null, true))
                       .ReturnsAsync(sampleProducts);

            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.GetAll(null, null, true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Single(returnedProducts);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WhenProductExists()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var productId = 1;

            var sampleProduct = new ProductDto { Id = productId, Name = "Test Product", Price = 100, CategoryName = "Electronics" };

            mockService.Setup(s => s.GetProductByIdAsync(productId))
                       .ReturnsAsync(sampleProduct);

            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(productId, returnedProduct.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var productId = 99;

            mockService.Setup(s => s.GetProductByIdAsync(productId))
                       .ReturnsAsync((ProductDto?)null);

            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
