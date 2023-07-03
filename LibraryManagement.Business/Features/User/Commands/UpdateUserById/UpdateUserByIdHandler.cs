using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;

namespace LibraryManagement.Business.Features.User.Commands.UpdateUserById
{
    public class UpdateUserByIdHandler : IRequestHandler<UpdateUserByIdRequest, BaseResponse<UpdateUserByIdResponse>>
    {
        private readonly LibraryManagementDbContext _context;

        public UpdateUserByIdHandler(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<UpdateUserByIdResponse>> Handle(UpdateUserByIdRequest request,
            CancellationToken cancellationToken)
        {
            // Find the user by their ID
            Domain.Entities.User? user = await _context.Users.FindAsync(request.Id);

            if (user is null)
                throw new CustomException(ErrorHandling.ErrorType.UserNotFound); // Throw an exception if the user is not found

            // Update the user's properties with the provided values
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Gsm = request.Gsm;
            user.Address = request.Address;

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<UpdateUserByIdResponse>
            {
                Message = "User updated successfully",
            };
        }
    }
}
