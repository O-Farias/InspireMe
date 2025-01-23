using InspireMe.API.Controllers;
using InspireMe.API.Models;
using InspireMe.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace InspireMe.Tests.Controllers
{
    public class QuotesControllerTests
    {
        private readonly QuotesController _controller;
        private readonly Mock<IQuotesService> _mockQuotesService;

        public QuotesControllerTests()
        {
            // Mock do serviço
            _mockQuotesService = new Mock<IQuotesService>();

            // Instância do controlador com o mock do serviço
            _controller = new QuotesController(_mockQuotesService.Object);
        }

        [Fact]
        public void Get_ShouldReturnAllQuotes()
        {
            // Arrange
            var mockQuotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "Frase 1", Author = "Autor 1" },
                new Quote { Id = 2, Text = "Frase 2", Author = "Autor 2" }
            };

            _mockQuotesService.Setup(s => s.GetAllQuotes()).Returns(mockQuotes);

            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(mockQuotes, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectQuote()
        {
            // Arrange
            var mockQuote = new Quote { Id = 1, Text = "Frase Teste", Author = "Autor Teste" };

            _mockQuotesService.Setup(s => s.GetQuoteById(1)).Returns(mockQuote);

            // Act
            var result = _controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(mockQuote, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenQuoteDoesNotExist()
        {
            // Arrange
            _mockQuotesService.Setup(s => s.GetQuoteById(It.IsAny<int>())).Returns((Quote)null);

            // Act
            var result = _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnCreatedQuote()
        {
            // Arrange
            var newQuote = new Quote { Text = "Frase Nova", Author = "Autor Novo" };
            var addedQuote = new Quote { Id = 3, Text = "Frase Nova", Author = "Autor Novo" };

            _mockQuotesService.Setup(s => s.AddQuote(newQuote)).Returns(addedQuote);

            // Act
            var result = _controller.Add(newQuote) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(addedQuote, result.Value);
        }

        [Fact]
        public void GetByAuthor_ShouldReturnQuotesForAuthor()
        {
            // Arrange
            var mockQuotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "Frase 1", Author = "Autor Teste" },
                new Quote { Id = 2, Text = "Frase 2", Author = "Autor Teste" }
            };

            _mockQuotesService.Setup(s => s.GetQuotesByAuthor("Autor Teste")).Returns(mockQuotes);

            // Act
            var result = _controller.GetByAuthor("Autor Teste") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(mockQuotes, result.Value);
        }

        [Fact]
        public void GetByAuthor_ShouldReturnNotFound_WhenNoQuotesExistForAuthor()
        {
            // Arrange
            _mockQuotesService.Setup(s => s.GetQuotesByAuthor(It.IsAny<string>())).Returns(new List<Quote>());

            // Act
            var result = _controller.GetByAuthor("Autor Desconhecido");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNoContent_WhenQuoteIsUpdated()
        {
            // Arrange
            var updatedQuote = new Quote { Text = "Frase Atualizada", Author = "Autor Atualizado" };

            _mockQuotesService.Setup(s => s.UpdateQuote(1, updatedQuote)).Returns(true);

            // Act
            var result = _controller.Update(1, updatedQuote);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFound_WhenQuoteDoesNotExist()
        {
            // Arrange
            var updatedQuote = new Quote { Text = "Frase Atualizada", Author = "Autor Atualizado" };

            _mockQuotesService.Setup(s => s.UpdateQuote(99, updatedQuote)).Returns(false);

            // Act
            var result = _controller.Update(99, updatedQuote);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenQuoteIsDeleted()
        {
            // Arrange
            _mockQuotesService.Setup(s => s.DeleteQuote(1)).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenQuoteDoesNotExist()
        {
            // Arrange
            _mockQuotesService.Setup(s => s.DeleteQuote(99)).Returns(false);

            // Act
            var result = _controller.Delete(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
