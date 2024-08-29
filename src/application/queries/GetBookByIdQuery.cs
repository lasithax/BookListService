using Bookshop_be.src.application.DTOs;
using MediatR;

namespace Bookshop_be.src.application.Queries
{
    public class GetBookByIdQuery : IRequest<BookDto>
    {
        public int Id { get; }

        public GetBookByIdQuery(int id)
        {
            Id = id;
        }
    }
}