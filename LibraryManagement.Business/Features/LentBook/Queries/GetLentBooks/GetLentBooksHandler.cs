using System.Globalization;
using System.Linq.Dynamic.Core;
using LibraryManagement.Core;
using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Business.Features.LentBook.Queries.GetLentBooks
{
    public class GetLentBooksHandler : IRequestHandler<GetLentBooksRequest, GetLentBooksResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryManagementDbContext _context;

        public GetLentBooksHandler(LibraryManagementDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetLentBooksResponse> Handle(GetLentBooksRequest request, CancellationToken cancellationToken)
        {
            // Initialize variables for filtering and pagination
            int filterRecord = 0;

            // Get the HTTP request
            HttpRequest? req = _httpContextAccessor.HttpContext?.Request;

            // Get the DataTables parameters from the request
            string? draw = req.Form["draw"].FirstOrDefault();
            string? sortColumn = req.Form["columns[" + req.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault(); // Author
            var sortColumnDirection = req.Form["order[0][dir]"].FirstOrDefault(); // desc
            var searchValue = req.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(req.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(req.Form["start"].FirstOrDefault() ?? "0");

            // Create a queryable for LentBook entities, including related User and Book entities, and filter by IsReturned == false
            IQueryable<Domain.Entities.LentBook> data = _context.Set<Domain.Entities.LentBook>().AsQueryable()
                .Include(x => x.User)
                .Include(x => x.Book).Where(x => x.IsReturned == false);

            int totalRecord = data.Count();

            // Search data when search value is provided
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                    x.Book.Name.ToLower().Contains(searchValue.ToLower())
                    || x.User.FullName.ToLower().Contains(searchValue.ToLower())
                    || x.CreatedDate.ToString(CultureInfo.InvariantCulture).ToLower().Contains(searchValue.ToLower())
                    || x.ReturnDate.ToString(CultureInfo.InvariantCulture).ToLower().Contains(searchValue.ToLower())
                );
            }

            // Get the total count of filtered records
            filterRecord = data.Count();

            // Sort the data by ReturnDate
            data = data.OrderBy(x => x.ReturnDate);

            // Perform pagination
            var lentBooks = data.Skip(skip).Take(pageSize).ToList();

            // Update the image URLs with the root URL
            lentBooks.Where(x => x.Book.ImageUrl != null).ToList()
                .ForEach(x => x.Book.ImageUrl = Paths.rootUrl + x.Book.ImageUrl);

            // Create the response object
            return new GetLentBooksResponse()
            {
                Draw = draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = lentBooks
            };
        }
    }
}
