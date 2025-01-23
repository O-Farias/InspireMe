using InspireMe.API.Models;

namespace InspireMe.API.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly List<Quote> _quotes;

        public QuotesService()
        {
            _quotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "O melhor jeito de prever o futuro é criá-lo.", Author = "Peter Drucker" },
                new Quote { Id = 2, Text = "A vida é o que acontece enquanto você está ocupado fazendo outros planos.", Author = "John Lennon" },
                new Quote { Id = 3, Text = "Seja a mudança que você deseja ver no mundo.", Author = "Mahatma Gandhi" },
                new Quote { Id = 4, Text = "Acredite que você pode, e você já está no meio do caminho.", Author = "Theodore Roosevelt" },
                new Quote { Id = 5, Text = "A única maneira de fazer um excelente trabalho é amar o que você faz.", Author = "Steve Jobs" }
            };
        }

        public IEnumerable<Quote> GetAllQuotes() => _quotes;

        public Quote? GetQuoteById(int id) => _quotes.FirstOrDefault(q => q.Id == id);

        public IEnumerable<Quote> GetQuotesByAuthor(string author) =>
            _quotes.Where(q => q.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

        public Quote AddQuote(Quote newQuote)
        {
            newQuote.Id = _quotes.Count + 1;
            _quotes.Add(newQuote);
            return newQuote;
        }

        public bool UpdateQuote(int id, Quote updatedQuote)
        {
            var existingQuote = GetQuoteById(id);
            if (existingQuote == null) return false;

            existingQuote.Text = updatedQuote.Text;
            existingQuote.Author = updatedQuote.Author;
            return true;
        }

        public bool DeleteQuote(int id)
        {
            var quote = GetQuoteById(id);
            if (quote == null) return false;

            _quotes.Remove(quote);
            return true;
        }
    }
}
