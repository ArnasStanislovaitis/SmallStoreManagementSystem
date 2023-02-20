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

    public class DetailsMethodTests
    {
        private readonly DbContextOptions<SmallStoreManagementSystemContext> _options;
        private readonly SmallStoreManagementSystemContext _context;

        public DetailsMethodTests()
        {
            _options = new DbContextOptionsBuilder<SmallStoreManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new SmallStoreManagementSystemContext(_options);
        } 

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenProductNotFound()
        {
            // Arrange
            var controller = new ProductsController(_context);

            // Act
            var result = await controller.Details(99999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }                
    }
}