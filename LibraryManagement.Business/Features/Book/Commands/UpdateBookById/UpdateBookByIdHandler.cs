using LibraryManagement.Core;
using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using LibraryManagement.Domain.Abstractions.Storage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Business.Features.Book.Commands.UpdateBookById
{
    // Represents the handler for updating a book by its ID.
    public class UpdateBookByIdHandler : IRequestHandler<UpdateBookByIdRequest, BaseResponse<UpdateBookByIdResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryManagementDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ILogger<UpdateBookByIdHandler> _logger;

        // Initializes a new instance of the UpdateBookByIdHandler class.
        public UpdateBookByIdHandler(IHttpContextAccessor httpContextAccessor, LibraryManagementDbContext context,
            IStorageService storageService, ILogger<UpdateBookByIdHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _storageService = storageService;
            _logger = logger;
        }

        // Handles the update book request.
        public async Task<BaseResponse<UpdateBookByIdResponse>> Handle(UpdateBookByIdRequest request,
            CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new CustomException(ErrorHandling.ErrorType.BookNotFound);

            string? imageUrl = null;
            if (request.Image != null)
            {
                imageUrl = await _storageService.UploadFileAsync(request.Image, Paths.BookFolder);
                if (imageUrl == null)
                    throw new CustomException(ErrorHandling.ErrorType.ImageUploadError);
                book.ImageUrl = imageUrl;
            }

            _logger.LogInformation($"{book.Name} updated {request.Name} successfully.");

            // Update the book properties with the values from the request.
            book.Name = request.Name;
            book.AuthorName = request.AuthorName;
            book.PublisherName = request.PublisherName;
            book.PageCount = request.PageCount;
            book.Category = request.Category;
            book.PublicationDate = request.PublicationDate;
            book.Stock = request.Stock;
            book.LoanedCount = request.LoanedCount;

            await _context.SaveChangesAsync(cancellationToken);

            // Create and return the response with the updated book information.
            return new BaseResponse<UpdateBookByIdResponse>
            {
                Message = "Book updated successfully",
                Data = new UpdateBookByIdResponse
                {
                    ImageUrl = imageUrl ?? book.ImageUrl,
                }
            };
        }
    }
}
