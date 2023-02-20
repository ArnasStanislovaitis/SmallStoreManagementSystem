using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallStoreManagementSystem.Data;
using SmallStoreManagementSystem;

namespace SmallStoreManagementSystemTests
{
    public class EditMethodTests
    {
        private readonly DbContextOptions<SmallStoreManagementSystemContext> _options;
        private readonly SmallStoreManagementSystemContext _context;

        public EditMethodTests()
        {
            _options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new SmallStoreManagementSystemContext(_options);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenProductNotFound()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Edit(99999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithProduct()
        {
            // Arrange
            var product = new Product { Name = "Product 1", Price = 10, Type = "aa", Subtype = "oo" };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Edit(product.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Product>(viewResult.ViewData.Model);
            Assert.Equal(product.Id, model.Id);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Price, model.Price);
            Assert.Equal(product.Type, model.Type);
            Assert.Equal(product.Subtype, model.Subtype);

            //Cleanup
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdDoesNotMatchProduct()
        {
            // Arrange
            var product = new Product { Name = "Product 1", Price = 10, Type = "aa", Subtype = "oo" };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var controller = new ProductsController(_context);
            var newProduct = new Product { Id = product.Id + 1, Name = "New Product", Price = 20, Type = "bb", Subtype = "pp" };

            // Act
            var result = await controller.Edit(product.Id, newProduct);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            //Cleanup
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }    
}