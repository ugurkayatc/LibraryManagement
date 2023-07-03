using LibraryManagement.Core.Response;
using MediatR;

namespace LibraryManagement.Business.Features.User.Commands.UpdateUserById
{
    public class UpdateUserByIdRequest : IRequest<BaseResponse<UpdateUserByIdResponse>>
    {
        public Guid Id { get; set; } // The ID of the user to be updated
        public string FullName { get; set; } // The updated full name of the user
        public string? Email { get; set; } // The updated email address of the user (nullable)
        public string? Gsm { get; set; } // The updated GSM (mobile) number of the user (nullable)
        public string? Address { get; set; } // The updated address of the user (nullable)
    }
}
