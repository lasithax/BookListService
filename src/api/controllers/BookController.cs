using Bookshop_be.src.application.Queries;
using Bookshop_be.src.application.DTOs;
using Bookshop_be.src.shared.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop_be.src.API.Controllers
{
    [Route("api/books-list")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks([FromQuery] BookFilterDto? filters)
        {
            // If filters is null, create a default one
            filters ??= new BookFilterDto();

            var books = await _mediator.Send(new GetBooksQuery(filters));
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                return Ok(book);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
