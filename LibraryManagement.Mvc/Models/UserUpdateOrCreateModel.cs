namespace LibraryManagement.Mvc.Models
{
    // Model class for user update or create operations
    public class UserUpdateOrCreateModel
    {
        public Guid? Id { get; set; } // Unique identifier for the user
        public string FullName { get; set; } // Full name of the user
        public string? Email { get; set; } // Email address of the user (optional)
        public string Gsm { get; set; } // GSM number of the user
        public string? Address { get; set; } // Address of the user (optional)
    }
}
