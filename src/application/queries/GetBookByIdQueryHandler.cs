using Bookshop_be.src.application.DTOs;
using Bookshop_be.src.infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshop_be.src.shared.Common;

namespace Bookshop_be.src.application.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly DataContext _context;

        public GetBookByIdQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Where(b => b.Id == request.Id)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Author = b.Author,
                    Price = b.Price,
                    Rating = b.Rating,
                    Category = b.Category
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (book == null)
            {
                // Handle case where the book is not found (e.g., throw an exception or return null)
                throw new NotFoundException($"Book with ID {request.Id} not found.");
            }

            return book;
        }
    }
}
