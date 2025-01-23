using InspireMe.API.Models;
using InspireMe.API.Services;
using Xunit;

namespace InspireMe.Tests.Services
{
    public class QuotesServiceTests
    {
        private readonly QuotesService _quotesService;

        public QuotesServiceTests()
        {
            // Instância do serviço que será testado
            _quotesService = new QuotesService();
        }

        [Fact]
        public void GetAllQuotes_ShouldReturnInitialQuotes()
        {
            // Act
            var quotes = _quotesService.GetAllQuotes();

            // Assert
            Assert.NotNull(quotes);
            Assert.Equal(5, quotes.Count()); // Inicialmente, são 5 frases
        }

        [Fact]
        public void GetQuoteById_ShouldReturnCorrectQuote()
        {
            // Act
            var quote = _quotesService.GetQuoteById(1);

            // Assert
            Assert.NotNull(quote);
            Assert.Equal("O melhor jeito de prever o futuro é criá-lo.", quote.Text);
        }

        [Fact]
        public void GetQuoteById_ShouldReturnNull_WhenQuoteDoesNotExist()
        {
            // Act
            var quote = _quotesService.GetQuoteById(99);

            // Assert
            Assert.Null(quote);
        }

        [Fact]
        public void AddQuote_ShouldAddNewQuoteToList()
        {
            // Arrange
            var newQuote = new Quote
            {
                Text = "A persistência é o caminho do êxito.",
                Author = "Charles Chaplin"
            };

            // Act
            var addedQuote = _quotesService.AddQuote(newQuote);

            // Assert
            Assert.NotNull(addedQuote);
            Assert.Equal(6, addedQuote.Id); // Deve ser o próximo ID (6)
            Assert.Equal("A persistência é o caminho do êxito.", addedQuote.Text);
        }
    }
}
