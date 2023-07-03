using MediatR;

namespace LibraryManagement.Business.Features.Book.Queries.GetAllBook
{
    // Represents the request model for the GetAllBook query.
    public class GetAllBookRequest : IRequest<GetAllBookResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
