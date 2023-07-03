namespace LibraryManagement.Business.Features.User.Commands.UpdateUserById
{
    public class UpdateUserByIdResponse
    {
        public Guid Id { get; set; } // The ID of the updated user
        public string FullName { get; set; } // The full name of the updated user
        public string? Email { get; set; } // The email address of the updated user (nullable)
        public string? Gsm { get; set; } // The GSM (mobile) number of the updated user (nullable)
        public string? Address { get; set; } // The address of the updated user (nullable)
    }
}
