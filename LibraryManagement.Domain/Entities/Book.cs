using LibraryManagement.Domain.Entities.Common;

namespace LibraryManagement.Domain.Entities
{
    public class Book : BaseEntity
    {
        // The name of the book
        public string Name { get; set; }

        // The name of the author of the book
        public string AuthorName { get; set; }

        // The name of the publisher of the book
        public string PublisherName { get; set; }

        // The total number of pages in the book
        public int PageCount { get; set; }

        // The category or genre of the book
        public string Category { get; set; }

        // The publication date of the book
        public DateTime PublicationDate { get; set; }

        // The number of available copies in the library's stock
        public int Stock { get; set; }

        // The number of times the book has been loaned out
        public int? LoanedCount { get; set; } = 0;

        // The URL of the book's image cover
        public string? ImageUrl { get; set; }
    }
}
