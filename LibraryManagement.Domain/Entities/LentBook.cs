using LibraryManagement.Domain.Entities.Common;

namespace LibraryManagement.Domain.Entities
{
    public class LentBook : BaseEntity
    {
        // The ID of the user who lent the book
        public Guid UserId { get; set; }

        // The ID of the lent book
        public Guid BookId { get; set; }

        // The return date of the lent book
        public DateTime ReturnDate { get; set; }

        // Indicates whether the book has been returned or not
        public bool IsReturned { get; set; }

        // The user who lent the book
        public User User { get; set; }

        // The lent book
        public Book Book { get; set; }
    }
}
