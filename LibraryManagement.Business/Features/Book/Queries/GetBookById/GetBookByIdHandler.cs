using LibraryManagement.Core;
using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Business.Features.Book.Queries.GetBookById;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdRequest, BaseResponse<GetBookByIdResponse>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private LibraryManagementDbContext _context;

    public GetBookByIdHandler(IHttpContextAccessor httpContextAccessor, LibraryManagementDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<BaseResponse<GetBookByIdResponse>> Handle(GetBookByIdRequest request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.Book book =
            (await _context.Books.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken))!;

        if (!string.IsNullOrEmpty(book.ImageUrl))
            book.ImageUrl = Paths.rootUrl + book.ImageUrl;

        return new BaseResponse<GetBookByIdResponse>
        {
            Data = new GetBookByIdResponse
            {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                PublisherName = book.PublisherName,
                PageCount = book.PageCount,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                Stock = book.Stock,
                LoanedCount = book.LoanedCount,
                ImageUrl = book.ImageUrl
            }
        };
    }
}