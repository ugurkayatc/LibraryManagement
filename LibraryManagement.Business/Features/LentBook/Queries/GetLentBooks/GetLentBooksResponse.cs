namespace LibraryManagement.Business.Features.LentBook.Queries.GetLentBooks
{
    // Represents the response for getting lent books
    public class GetLentBooksResponse
    {
        public string? Draw { get; set; } // The draw value for DataTables integration
        public int? RecordsTotal { get; set; } // The total number of records in the database
        public int RecordsFiltered { get; set; } // The number of records after filtering
        public List<Domain.Entities.LentBook>? Data { get; set; } // The list of lent books
    }
}
