using System.ComponentModel.DataAnnotations;

namespace Bookshop_be.src.application.DTOs
{
    public class BookFilterDto
    {
        public string? Search { get; set; }  // Nullable string
        public string? Category { get; set; }  // Nullable string
        public int? MinRating { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;  // Default to 1
        public int PageSize { get; set; } = 10;  // Default to 10
    }
}
