using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Mvc.Models
{
    public class LentBookCreateModel
    {
        // The ID of the user who lent the book
        public Guid UserId { get; set; }

        // The ID of the book being lent
        public Guid BookId { get; set; }

        // The return date of the lent book
        [Required(ErrorMessage = "Return Date is required")]
        public DateTime ReturnDate { get; set; }
    }
}
