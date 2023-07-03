using LibraryManagement.Core.Response;
using MediatR;
using System;

namespace LibraryManagement.Business.Features.LentBook.Commands.ReturnBook
{
    // Represents the request to return a lent book
    public class ReturnBookRequest : IRequest<BaseResponse<ReturnBookResponse>>
    {
        public Guid Id { get; set; } // The ID of the lent book to be returned
    }
}
