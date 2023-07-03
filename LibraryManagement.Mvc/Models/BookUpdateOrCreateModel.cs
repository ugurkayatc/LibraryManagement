using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Mvc.Models
{
    public class BookUpdateOrCreateModel
    {
        // ID of the book (nullable)
        public Guid? Id { get; set; }

        // Name of the book
        public string Name { get; set; }

        // Name of the author of the book
        public string AuthorName { get; set; }

        // Name of the publisher of the book
        public string PublisherName { get; set; }

        // Number of pages in the book (must be greater than 0)
        [Range(1, int.MaxValue, ErrorMessage = "PageCount must be greater than 0")]
        public int PageCount { get; set; }

        // Category or genre of the book
        public string Category { get; set; }

        // Publication date of the book
        public DateTime PublicationDate { get; set; }

        // Number of available copies of the book in the library's stock (must be greater than 0)
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be greater than 0")]
        public int Stock { get; set; }

        // Number of copies of the book currently on loan (nullable)
        public int? LoanedCount { get; set; }

        // URL of the book's image (nullable)
        public string? ImageUrl { get; set; }

        // Image file of the book (nullable)
        public IFormFile? Image { get; set; }
    }
}
