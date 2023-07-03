using MediatR;
// Importing the MediatR namespace for IRequest interface.

namespace LibraryManagement.Business.Features.User.Queries.GetUsers;
// The namespace where this code resides.

public class GetUsersRequest : IRequest<GetUsersResponse>
{
    public int Page { get; set; } = 0;
    // A property that represents the page number for pagination, initialized to 0.

    public int Size { get; set; } = 10;
    // A property that represents the number of items per page, initialized to 10.
}
