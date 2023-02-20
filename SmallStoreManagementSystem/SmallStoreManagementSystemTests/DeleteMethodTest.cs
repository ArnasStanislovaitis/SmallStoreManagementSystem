using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallStoreManagementSystem.Data;
using SmallStoreManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStoreManagementSystemTests
{
    public class DeleteMethodTests
    {
        private readonly DbContextOptions<SmallStoreManagementSystemContext> _options;
        private readonly SmallStoreManagementSystemContext _context;

        public DeleteMethodTests()
        {
            _options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;

            _context = new SmallStoreManagementSystemContext(_options);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductNotFound()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Delete(99999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenProductFound()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Add some test data to the database
            var product = new Product { Name = "Product 1", Price = 10, Type = "aa", Subtype = "oo" };
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await controller.Delete(product.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            Assert.Equal(product.Id, model.Id);

            // Cleanup
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }           
    }
}