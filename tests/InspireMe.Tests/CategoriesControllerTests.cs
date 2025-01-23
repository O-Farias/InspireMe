using InspireMe.API.Models;
using InspireMe.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InspireMe.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly CategoriesController _controller;
        private readonly Mock<ICategoriesService> _mockCategoriesService;

        public CategoriesControllerTests()
        {
            // Mock do serviço
            _mockCategoriesService = new Mock<ICategoriesService>();

            // Instância do controlador com o mock do serviço
            _controller = new CategoriesController(_mockCategoriesService.Object);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnAllCategories()
        {
            // Arrange
            var mockCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Technology", Description = "Tech topics" },
                new Category { Id = 2, Name = "Science", Description = "Science topics" }
            };

            _mockCategoriesService.Setup(s => s.GetAllCategoriesAsync()).ReturnsAsync(mockCategories);

            // Act
            var result = await _controller.GetCategories();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mockCategories, okResult.Value);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnCategoryById()
        {
            // Arrange
            var mockCategory = new Category { Id = 1, Name = "Technology", Description = "Tech topics" };

            _mockCategoriesService.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync(mockCategory);

            // Act
            var result = await _controller.GetCategory(1);
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mockCategory, okResult.Value);
        }

        [Fact]
        public async Task GetCategory_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockCategoriesService.Setup(s => s.GetCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync((Category?)null);

            // Act
            var result = await _controller.GetCategory(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnCreatedCategory()
        {
            // Arrange
            var newCategory = new Category { Name = "Technology", Description = "Tech topics" };
            var createdCategory = new Category { Id = 1, Name = "Technology", Description = "Tech topics" };

            _mockCategoriesService.Setup(s => s.CreateCategoryAsync(newCategory)).ReturnsAsync(createdCategory);

            // Act
            var result = await _controller.CreateCategory(newCategory);
            var createdAtResult = result.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(createdAtResult);
            Assert.Equal(201, createdAtResult.StatusCode);
            Assert.Equal(createdCategory, createdAtResult.Value);
        }

        [Fact]
        public async Task UpdateCategory_ShouldReturnNoContent_WhenCategoryIsUpdated()
        {
            // Arrange
            var updatedCategory = new Category { Name = "Updated Name", Description = "Updated Description" };

            _mockCategoriesService.Setup(s => s.UpdateCategoryAsync(1, updatedCategory)).ReturnsAsync(updatedCategory);

            // Act
            var result = await _controller.UpdateCategory(1, updatedCategory);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var updatedCategory = new Category { Name = "Updated Name", Description = "Updated Description" };

            _mockCategoriesService.Setup(s => s.UpdateCategoryAsync(99, updatedCategory)).ReturnsAsync((Category?)null);

            // Act
            var result = await _controller.UpdateCategory(99, updatedCategory);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnNoContent_WhenCategoryIsDeleted()
        {
            // Arrange
            _mockCategoriesService.Setup(s => s.DeleteCategoryAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockCategoriesService.Setup(s => s.DeleteCategoryAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCategory(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
