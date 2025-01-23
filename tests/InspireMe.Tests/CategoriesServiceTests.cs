using InspireMe.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InspireMe.Tests.Services
{
    public class CategoriesServiceTests
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesServiceTests()
        {
            _categoriesService = new CategoriesService();
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Act
            var categories = await _categoriesService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(categories);
            Assert.Empty(categories);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldAddNewCategory()
        {
            // Arrange
            var newCategory = new Category { Name = "Technology", Description = "All tech-related topics" };

            // Act
            var createdCategory = await _categoriesService.CreateCategoryAsync(newCategory);
            var categories = await _categoriesService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(createdCategory);
            Assert.Equal(1, createdCategory.Id);
            Assert.Equal("Technology", createdCategory.Name);
            Assert.Single(categories);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCorrectCategory()
        {
            // Arrange
            var newCategory = new Category { Name = "Technology", Description = "All tech-related topics" };
            await _categoriesService.CreateCategoryAsync(newCategory);

            // Act
            var category = await _categoriesService.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(category);
            Assert.Equal("Technology", category.Name);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Act
            var category = await _categoriesService.GetCategoryByIdAsync(99);

            // Assert
            Assert.Null(category);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldUpdateExistingCategory()
        {
            // Arrange
            var newCategory = new Category { Name = "Technology", Description = "All tech-related topics" };
            await _categoriesService.CreateCategoryAsync(newCategory);

            var updatedCategory = new Category { Name = "Tech", Description = "Updated description" };

            // Act
            var result = await _categoriesService.UpdateCategoryAsync(1, updatedCategory);
            var category = await _categoriesService.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tech", category.Name);
            Assert.Equal("Updated description", category.Description);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var updatedCategory = new Category { Name = "Tech", Description = "Updated description" };

            // Act
            var result = await _categoriesService.UpdateCategoryAsync(99, updatedCategory);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldRemoveCategory_WhenCategoryExists()
        {
            // Arrange
            var newCategory = new Category { Name = "Technology", Description = "All tech-related topics" };
            await _categoriesService.CreateCategoryAsync(newCategory);

            // Act
            var result = await _categoriesService.DeleteCategoryAsync(1);
            var categories = await _categoriesService.GetAllCategoriesAsync();

            // Assert
            Assert.True(result);
            Assert.Empty(categories);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
            // Act
            var result = await _categoriesService.DeleteCategoryAsync(99);

            // Assert
            Assert.False(result);
        }
    }
}
