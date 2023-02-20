using Microsoft.AspNetCore.Mvc;
using SmallStoreManagementSystem.Data;
using SmallStoreManagementSystem;
using Moq;
using SmallStoreManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SmallStoreManagementSystemTests
{

    public class CreateMethodTests
    {
        private readonly DbContextOptions<SmallStoreManagementSystemContext> _options;
        private readonly SmallStoreManagementSystemContext _context;

        public CreateMethodTests()
        {
            _options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new SmallStoreManagementSystemContext(_options);
        }  
        
        [Fact]
        public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new ProductsController(_context);
            var product = new Product { Id = 1, Name = "Test Product", Price = -1 }; // invalid model state

            // Add the product to the model state so that it is considered invalid
            controller.ModelState.AddModelError("Price", "Price must be a positive number");

            // Act
            var result = await controller.Create(product);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Product>(viewResult.Model);
            Assert.Equal(product, model);
        }
        [Fact]
        public async Task Create_AddsProductToDatabase_WhenModelIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new SmallStoreManagementSystemContext(options);

            var product = new Product
            {
                Name = "New Product",
                Price = 9.99m,
                Type = "Type",
                Subtype = "Subtype"
            };

            var controller = new ProductsController(context);

            // Act
            var result = await controller.Create(product) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            var savedProduct = context.Product.First(p => p.Name == product.Name);
            Assert.Equal(product.Price, savedProduct.Price);
            Assert.Equal(product.Type, savedProduct.Type);
            Assert.Equal(product.Subtype, savedProduct.Subtype);
        }
    }
}