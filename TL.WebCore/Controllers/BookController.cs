using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TL.Contracts.Models;
using TL.Contracts.Services;
using TL.WebCore.Validators;

namespace TL.WebInit.Controllers
{
    [ApiController]
    [Route("/api/books/")]
    public class BookController : ControllerBase
    {
        private const string ActionName = "Item successfully added";

        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        private readonly IValidator<BookModel> _bookValidator;

        public BookController(ILogger<BookController> logger, IBookService bookService, IValidator<BookModel> bookValidator)
        {
            _logger = logger;
            _bookService = bookService ;
            _bookValidator = bookValidator;
        }


        [HttpGet]
        [Route("{itemId}")]
        public IActionResult Get(int itemId)
        {
            var result = _bookService.GetBook(itemId);

            if (result.Data == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Post(BookModel item)
        {
            if (_bookValidator.Validate(item, out List<string> errors))
            {
                // Return a BadRequest response with the validation errors
                return BadRequest(new { errors });
            }

            var result = _bookService.AddBook(item);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] BookModel item)
        {
            if (_bookValidator.Validate(item, out List<string> errors))
            {
                // Return a BadRequest response with the validation errors
                return BadRequest(new { errors });
            }

            var result = _bookService.UpdateBook(item);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("Item was successfully updated", nameof(result.Data));
        }

        [HttpDelete]
        [Route("{itemId}")]
        public IActionResult Delete([FromRoute] int itemId)
        {
            var result = _bookService.DeleteBook(itemId);

            if (!result.Success)
            {
                return BadRequest(new { result.ErrorMessage });
            }
          
            return Ok(new { message = $"Item {itemId} was successfully deleted" });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookService.GetBooks();

            return Ok(result.Data);
        }
    }
}
