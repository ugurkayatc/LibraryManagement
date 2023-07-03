using LibraryManagement.Core.Response;
using MediatR;
using System;

namespace LibraryManagement.Business.Features.LentBook.Commands.CreateLentBook
{
    // Represents the request to create a lent book
    public class CreateLentBookRequest : IRequest<BaseResponse<CreateLentBookResponse>>
    {
        // The ID of the user
        public Guid UserId { get; set; }

        // The ID of the book
        public Guid BookId { get; set; }

        // The return date for the lent book
        public DateTime ReturnDate { get; set; }
    }
}
