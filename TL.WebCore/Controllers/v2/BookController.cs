using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TL.Contracts.Models;
using TL.Contracts.Queries;
using TL.Contracts.Services;
using TL.WebCore.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TL.WebCore.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;
        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{BookId}")]
        public IActionResult GetProductById([FromRoute] GetBookQuery query) => Ok(_mediator.Send(query).Result);
        /*
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookCommand command)
        {
             _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll() => _mediator.Send(new GetBooksQuery());
        */
    }
}
