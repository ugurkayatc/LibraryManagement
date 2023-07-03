using LibraryManagement.Core.Response;
using MediatR;

namespace LibraryManagement.Business.Features.User.Queries.GetUserById
{
    public class GetUserByIdRequest : IRequest<BaseResponse<GetUserByIdResponse>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }
    }
}
