using Bookshop_be.src.application.DTOs;
using Bookshop_be.src.infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_be.src.application.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly DataContext _context;

        public GetBooksQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filters.Search))
            {
                query = query.Where(b => b.Title.Contains(request.Filters.Search) || b.Author.Contains(request.Filters.Search));
            }

            if (!string.IsNullOrEmpty(request.Filters.Category))
            {
                query = query.Where(b => b.Category == request.Filters.Category);
            }

            if (request.Filters.MinRating.HasValue)
            {
                query = query.Where(b => b.Rating >= request.Filters.MinRating);
            }

            if (request.Filters.MinPrice.HasValue)
            {
                query = query.Where(b => b.Price >= request.Filters.MinPrice);
            }

            if (request.Filters.MaxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= request.Filters.MaxPrice);
            }

            var books = await query
            .Skip((request.Filters.PageNumber - 1) * request.Filters.PageSize)
            .Take(request.Filters.PageSize)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Author = b.Author,
                Price = b.Price,
                Rating = b.Rating,
                Category = b.Category,
                ImagePath = b.ImagePath
            })
            .ToListAsync(cancellationToken);

            return books;
        }
    }
}
