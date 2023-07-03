namespace LibraryManagement.Business.Features.Book.Queries.GetAllBook
{
    // Represents the response model for the GetAllBook query.
    public class GetAllBookResponse
    {
        public string? Draw { get; set; }
        public int? RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<Domain.Entities.Book>? Data { get; set; }
    }
}
