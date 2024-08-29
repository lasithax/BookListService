using Bookshop_be.src.application.DTOs;
using MediatR;

namespace Bookshop_be.src.application.Queries
{
    public class GetBooksQuery : IRequest<List<BookDto>>
    {
        public BookFilterDto Filters { get; }

        public GetBooksQuery(BookFilterDto filters)
        {
            Filters = filters;
        }
    }
}
