using InspireMe.API.Models; 
using System.Collections.Generic; 

namespace InspireMe.API.Services
{
    public interface IQuotesService
    {
        IEnumerable<Quote> GetAllQuotes();
        Quote? GetQuoteById(int id);
        Quote AddQuote(Quote newQuote);
    }
}
