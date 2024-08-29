using Bookshop_be.src.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_be.src.infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
