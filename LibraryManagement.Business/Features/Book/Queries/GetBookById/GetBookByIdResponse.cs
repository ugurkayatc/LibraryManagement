namespace LibraryManagement.Business.Features.Book.Queries.GetBookById
{
    // Represents the response containing book information
    public class GetBookByIdResponse
    {
        // The ID of the book
        public Guid Id { get; set; }

        // The name of the book
        public string Name { get; set; }

        // The author of the book
        public string AuthorName { get; set; }

        // The publisher of the book
        public string PublisherName { get; set; }

        // The page count of the book
        public int PageCount { get; set; }

        // The category of the book
        public string Category { get; set; }

        // The publication date of the book
        public DateTime PublicationDate { get; set; }

        // The stock count of the book
        public int Stock { get; set; }

        // The loaned count of the book
        public int? LoanedCount { get; set; } = 0;

        // The URL of the book's image
        public string? ImageUrl { get; set; }
    }
}
