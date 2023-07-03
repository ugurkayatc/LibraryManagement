using LibraryManagement.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Business.Features.Book.Commands.AddNewBook
{
    // Represents a request to add a new book.
    public class AddNewBookRequest : IRequest<BaseResponse<AddNewBookResponse>>
    {
        // Gets or sets the unique identifier of the book.
        public Guid Id { get; set; }

        // Gets or sets the name of the book.
        public string Name { get; set; }

        // Gets or sets the author's name of the book.
        public string AuthorName { get; set; }

        // Gets or sets the publisher's name of the book.
        public string PublisherName { get; set; }

        // Gets or sets the page count of the book.
        public int PageCount { get; set; }

        // Gets or sets the category of the book.
        public string Category { get; set; }

        // Gets or sets the publication date of the book.
        public DateTime PublicationDate { get; set; }

        // Gets or sets the stock count of the book.
        public int Stock { get; set; }

        // Gets or sets the loaned count of the book.
        public int? LoanedCount { get; set; }

        // Gets or sets the URL of the book's image.
        public string? ImageUrl { get; set; }

        // Gets or sets the image file of the book.
        public IFormFile? Image { get; set; }
    }
}
