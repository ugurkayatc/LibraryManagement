namespace LibraryManagement.Business.Features.Book.Commands.UpdateBookById
{
    // Represents the response for updating a book by its ID.
    public class UpdateBookByIdResponse
    {
        // Gets or sets the unique identifier of the updated book.
        public Guid Id { get; set; }

        // Gets or sets the updated name of the book.
        public string Name { get; set; }

        // Gets or sets the updated author's name of the book.
        public string AuthorName { get; set; }

        // Gets or sets the updated publisher's name of the book.
        public string PublisherName { get; set; }

        // Gets or sets the updated page count of the book.
        public int PageCount { get; set; }

        // Gets or sets the updated category of the book.
        public string Category { get; set; }

        // Gets or sets the updated publication date of the book.
        public DateTime PublicationDate { get; set; }

        // Gets or sets the updated stock count of the book.
        public int Stock { get; set; }

        // Gets or sets the updated loaned count of the book.
        public int? LoanedCount { get; set; }

        // Gets or sets the updated URL of the book's image.
        public string? ImageUrl { get; set; }
    }
}
