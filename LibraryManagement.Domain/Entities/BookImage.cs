using LibraryManagement.Domain.Entities.Common;

namespace LibraryManagement.Domain.Entities
{
    public class BookImage : BaseEntity
    {
        // The URL of the book image
        public string Url { get; set; }

        // The associated book for the image
        public Book Book { get; set; }
    }
}
