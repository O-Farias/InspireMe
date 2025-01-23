using Microsoft.AspNetCore.Mvc;
using InspireMe.API.Models;
using InspireMe.API.Services;

namespace InspireMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;

        public QuotesController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_quotesService.GetAllQuotes());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quote = _quotesService.GetQuoteById(id);
            return quote != null ? Ok(quote) : NotFound();
        }

        [HttpGet("author/{author}")]
        public IActionResult GetByAuthor(string author)
        {
            var quotes = _quotesService.GetQuotesByAuthor(author);
            return quotes.Any() ? Ok(quotes) : NotFound(new { Message = "Nenhuma frase encontrada para esse autor." });
        }

        [HttpPost]
        public IActionResult Add(Quote newQuote)
        {
            var addedQuote = _quotesService.AddQuote(newQuote);
            return CreatedAtAction(nameof(GetById), new { id = addedQuote.Id }, addedQuote);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Quote updatedQuote)
        {
            var success = _quotesService.UpdateQuote(id, updatedQuote);
            return success ? NoContent() : NotFound(new { Message = "Frase não encontrada para atualização." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _quotesService.DeleteQuote(id);
            return success ? NoContent() : NotFound(new { Message = "Frase não encontrada para exclusão." });
        }
    }
}
