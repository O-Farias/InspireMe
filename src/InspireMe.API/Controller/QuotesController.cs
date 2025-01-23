using Microsoft.AspNetCore.Mvc;
using InspireMe.API.Models;

namespace InspireMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private static readonly List<Quote> Quotes = new List<Quote>
        {
            new Quote { Id = 1, Text = "O melhor jeito de prever o futuro é criá-lo.", Author = "Peter Drucker" },
            new Quote { Id = 2, Text = "A vida é o que acontece enquanto você está ocupado fazendo outros planos.", Author = "John Lennon" },
            new Quote { Id = 3, Text = "Seja a mudança que você deseja ver no mundo.", Author = "Mahatma Gandhi" },
            new Quote { Id = 4, Text = "Acredite que você pode, e você já está no meio do caminho.", Author = "Theodore Roosevelt" },
            new Quote { Id = 5, Text = "A única maneira de fazer um excelente trabalho é amar o que você faz.", Author = "Steve Jobs" }
        };

        [HttpGet]
        public IActionResult Get() => Ok(Quotes);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quote = Quotes.FirstOrDefault(q => q.Id == id);
            return quote != null ? Ok(quote) : NotFound();
        }

        [HttpPost]
        public IActionResult Add(Quote newQuote)
        {
            newQuote.Id = Quotes.Count + 1;
            Quotes.Add(newQuote);
            return CreatedAtAction(nameof(GetById), new { id = newQuote.Id }, newQuote);
        }
    }
}
