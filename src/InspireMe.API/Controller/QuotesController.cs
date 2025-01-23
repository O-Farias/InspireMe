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

        [HttpPost]
        public IActionResult Add(Quote newQuote)
        {
            var addedQuote = _quotesService.AddQuote(newQuote);
            return CreatedAtAction(nameof(GetById), new { id = addedQuote.Id }, addedQuote);
        }
    }
}
