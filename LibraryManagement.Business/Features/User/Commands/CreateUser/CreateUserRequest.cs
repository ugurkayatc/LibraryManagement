using LibraryManagement.Core.Response;
using MediatR;

namespace LibraryManagement.Business.Features.User.Commands.CreateUser
{
    // Represents a request to create a user
    public class CreateUserRequest : IRequest<BaseResponse<CreateUserResponse>>
    {
        public string FullName { get; set; } // The full name of the user (required)
        public string? Email { get; set; } // The email address of the user (optional)
        public string? Gsm { get; set; } // The GSM (mobile phone) number of the user (optional)
        public string? Address { get; set; } // The address of the user (optional)
    }
}
