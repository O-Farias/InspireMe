using InspireMe.API.Models;
using System.Collections.Generic;

namespace InspireMe.API.Services
{
    public interface IQuotesService
    {
        IEnumerable<Quote> GetAllQuotes();
        Quote? GetQuoteById(int id);
        IEnumerable<Quote> GetQuotesByAuthor(string author);
        Quote AddQuote(Quote newQuote);
        bool UpdateQuote(int id, Quote updatedQuote);
        bool DeleteQuote(int id);
    }
}
