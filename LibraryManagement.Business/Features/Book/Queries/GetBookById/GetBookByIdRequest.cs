using LibraryManagement.Core.Response;
using MediatR;

namespace LibraryManagement.Business.Features.Book.Queries.GetBookById
{
    // Represents a request to get a book by its ID
    public class GetBookByIdRequest : IRequest<BaseResponse<GetBookByIdResponse>>
    {
        // The ID of the book to retrieve
        public Guid Id { get; set; }
    }
}
