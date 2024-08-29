namespace Bookshop_be.src.domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public required string Category { get; set; }

        public string? ImagePath { get; set; }
    }
}
