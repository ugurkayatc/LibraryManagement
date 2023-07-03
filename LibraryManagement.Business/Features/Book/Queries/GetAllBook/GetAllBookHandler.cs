using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using LibraryManagement.Core;

namespace LibraryManagement.Business.Features.Book.Queries.GetAllBook
{
    public class GetAllBookHandler : IRequestHandler<GetAllBookRequest, GetAllBookResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryManagementDbContext _context;

        public GetAllBookHandler(IHttpContextAccessor httpContextAccessor, LibraryManagementDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<GetAllBookResponse> Handle(GetAllBookRequest request, CancellationToken cancellationToken)
        {
            int filterRecord = 0;

            HttpRequest? req = _httpContextAccessor.HttpContext?.Request;

            // Extract information from the HTTP request
            string? draw = req.Form["draw"].FirstOrDefault();
            string? sortColumn = req.Form["columns[" + req.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDirection = req.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = req.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(req.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(req.Form["start"].FirstOrDefault() ?? "0");

            // Retrieve all books from the database
            IQueryable<Domain.Entities.Book> data = _context.Set<Domain.Entities.Book>().AsQueryable();

            int totalRecord = data.Count();

            // Apply search filter if a search value is provided
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                    x.Name.ToLower().Contains(searchValue.ToLower())
                    || x.AuthorName.ToLower().Contains(searchValue.ToLower())
                    || x.Category.ToLower().Contains(searchValue.ToLower())
                    || x.PublisherName.ToLower().Contains(searchValue.ToLower())
                );
            }

            // Get total count of filtered records
            filterRecord = data.Count();

            // Sort the data based on the provided sort column and direction
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                data = data.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            // Perform pagination
            var books = data.Skip(skip).Take(pageSize).ToList();

            // Update image URLs by appending the root URL
            books.Where(x => x.ImageUrl != null).ToList().ForEach(x => x.ImageUrl = Paths.rootUrl + x.ImageUrl);

            return new GetAllBookResponse
            {
                Draw = draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = books
            };
        }
    }
}
