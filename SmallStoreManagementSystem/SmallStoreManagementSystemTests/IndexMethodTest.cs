using Microsoft.AspNetCore.Mvc;
using SmallStoreManagementSystem.Data;
using SmallStoreManagementSystem;
using Microsoft.EntityFrameworkCore;


namespace SmallStoreManagementSystemTests
{

    public class IndexMethodTests
    {
        private readonly DbContextOptions<SmallStoreManagementSystemContext> _options;
        private readonly SmallStoreManagementSystemContext _context;

        public IndexMethodTests()
        {
            _options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new SmallStoreManagementSystemContext(_options);
        }        

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Add some test data to the database
            _context.Product.AddRange(
                new Product { Name = "Product 1", Price = 10,Type="aaa",Subtype="ohoo" },
                new Product { Name = "Product 2", Price = 20, Type = "aa", Subtype = "ooo" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());

            //Cleanup
            _context.Product.RemoveRange(_context.Product);
            await _context.SaveChangesAsync();
        }
    }
}