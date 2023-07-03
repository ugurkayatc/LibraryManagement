using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Business.Features.User.Commands.CreateUser
{
    // Represents a handler for creating a user
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, BaseResponse<CreateUserResponse>>
    {
        private readonly LibraryManagementDbContext _context; // Database context for accessing user data
        private readonly ILogger<CreateUserHandler> _logger; // Logger for logging information

        public CreateUserHandler(LibraryManagementDbContext context, ILogger<CreateUserHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Handles the create user request and creates a new user in the database
        public async Task<BaseResponse<CreateUserResponse>> Handle(CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            // Create a new user entity and set its properties from the request
            await _context.Users.AddAsync(new Domain.Entities.User
            {
                FullName = request.FullName,
                Email = request.Email,
                Gsm = request.Gsm,
                Address = request.Address
            }, cancellationToken);

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Log the success message
            _logger.LogInformation($"{request.FullName} add User successfully.");

            // Return a base response indicating that the user was created successfully
            return new BaseResponse<CreateUserResponse>
            {
                Message = "User created successfully."
            };
        }
    }
}
