using LibraryManagement.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Business.Features.Book.Commands.UpdateBookById
{
    // Represents a request to update a book by its ID.
    public class UpdateBookByIdRequest : IRequest<BaseResponse<UpdateBookByIdResponse>>
    {
        // Gets or sets the unique identifier of the book.
        public Guid Id { get; set; }

        // Gets or sets the new name of the book.
        public string Name { get; set; }

        // Gets or sets the new author's name of the book.
        public string AuthorName { get; set; }

        // Gets or sets the new publisher's name of the book.
        public string PublisherName { get; set; }

        // Gets or sets the new page count of the book.
        public int PageCount { get; set; }

        // Gets or sets the new category of the book.
        public string Category { get; set; }

        // Gets or sets the new publication date of the book.
        public DateTime PublicationDate { get; set; }

        // Gets or sets the new stock count of the book.
        public int Stock { get; set; }

        // Gets or sets the new loaned count of the book.
        public int? LoanedCount { get; set; } = 0;

        // Gets or sets the new URL of the book's image.
        public string? ImageUrl { get; set; }

        // Gets or sets the new image file of the book.
        public IFormFile? Image { get; set; }
    }
}
