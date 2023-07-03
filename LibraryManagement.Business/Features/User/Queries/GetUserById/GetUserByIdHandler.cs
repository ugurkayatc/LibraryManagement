using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;

namespace LibraryManagement.Business.Features.User.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, BaseResponse<GetUserByIdResponse>>
    {
        private readonly LibraryManagementDbContext _context;

        public GetUserByIdHandler(LibraryManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the request to get a user by their unique identifier.
        /// </summary>
        /// <param name="request">The request object containing the user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the user details.</returns>
        public async Task<BaseResponse<GetUserByIdResponse>> Handle(GetUserByIdRequest request,
            CancellationToken cancellationToken)
        {
            // Find the user in the database by ID
            Domain.Entities.User? user = _context.Users.Find(request.Id);

            // If user is not found, throw a custom exception
            if (user is null)
                throw new CustomException(ErrorHandling.ErrorType.UserNotFound);

            return new BaseResponse<GetUserByIdResponse>
            {
                Data = new GetUserByIdResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Gsm = user.Gsm,
                    Address = user.Address
                }
            };
        }
    }
}
