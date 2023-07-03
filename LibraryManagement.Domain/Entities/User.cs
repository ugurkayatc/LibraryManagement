using LibraryManagement.Domain.Entities.Common;

namespace LibraryManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        // The full name of the user
        public string FullName { get; set; }

        // The email address of the user (optional)
        public string? Email { get; set; }

        // The GSM (mobile phone) number of the user (optional)
        public string? Gsm { get; set; }

        // The address of the user (optional)
        public string? Address { get; set; }
    }
}
