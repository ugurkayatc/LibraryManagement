using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;

namespace LibraryManagement.Business.Features.User.Queries.GetUsers;
// The namespace where this code resides.

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LibraryManagementDbContext _context;

    public GetUsersHandler(IHttpContextAccessor httpContextAccessor, LibraryManagementDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        int filterRecord = 0;

        HttpRequest? req = _httpContextAccessor.HttpContext?.Request;
        // Accessing the current HTTP request through IHttpContextAccessor.

        string? draw = req.Form["draw"].FirstOrDefault();
        // Getting the value of the "draw" parameter from the request's form data.

        string? sortColumn = req.Form["columns[" + req.Form["order[0][column]"].FirstOrDefault() + "][name]"]
            .FirstOrDefault(); // Author
        // Getting the sort column name from the request's form data.

        var sortColumnDirection = req.Form["order[0][dir]"].FirstOrDefault(); // desc
        // Getting the sort direction from the request's form data.

        var searchValue = req.Form["search[value]"].FirstOrDefault();
        // Getting the search value from the request's form data.

        int pageSize = Convert.ToInt32(req.Form["length"].FirstOrDefault() ?? "0");
        // Getting the page size from the request's form data.

        int skip = Convert.ToInt32(req.Form["start"].FirstOrDefault() ?? "0");
        // Getting the number of items to skip from the request's form data.

        IQueryable<Domain.Entities.User> data = _context.Set<Domain.Entities.User>().AsQueryable();
        // Creating an IQueryable<User> object from the User entity set in the context.

        int totalRecord = data.Count();
        // Counting the total number of records in the data set.

        if (!string.IsNullOrEmpty(searchValue))
        {
            data = data.Where(x =>
                x.FullName.ToLower().Contains(searchValue.ToLower())
                || x.Email.ToLower().Contains(searchValue.ToLower())
                || x.Gsm.ToLower().Contains(searchValue.ToLower())
            );
            // Applying a search filter to the data based on the search value.
        }

        filterRecord = data.Count();
        // Counting the number of records after applying the filter.

        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
        {
            data = data.OrderBy(sortColumn + " " + sortColumnDirection);
            // Applying sorting to the data based on the sort column and direction.
        }

        List<Domain.Entities.User> userList = data.Skip(skip).Take(pageSize).ToList();
        // Getting a list of users based on pagination.

        return Task.FromResult(new GetUsersResponse
        {
            Draw = draw,
            RecordsTotal = totalRecord,
            RecordsFiltered = filterRecord,
            Data = userList
        });
        // Returning a new instance of GetUsersResponse with the populated data.
    }
}
