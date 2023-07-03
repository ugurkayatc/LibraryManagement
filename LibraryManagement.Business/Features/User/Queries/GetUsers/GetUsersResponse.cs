namespace LibraryManagement.Business.Features.User.Queries.GetUsers;
// The namespace where this code resides.

public class GetUsersResponse
{
    public string? Draw { get; set; }
    // A property that represents a draw value as a string, can be nullable.

    public int? RecordsTotal { get; set; }
    // A property that represents the total number of records, can be nullable.

    public int RecordsFiltered { get; set; }
    // A property that represents the number of records after filtering.

    public List<Domain.Entities.User>? Data { get; set; }
    // A property that represents a list of User entities, can be nullable.
}
