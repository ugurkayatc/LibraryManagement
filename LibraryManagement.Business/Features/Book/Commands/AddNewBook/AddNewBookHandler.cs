using LibraryManagement.Core;
using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using LibraryManagement.Domain.Abstractions.Storage;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Business.Features.Book.Commands.AddNewBook;

public class AddNewBookHandler : IRequestHandler<AddNewBookRequest, BaseResponse<AddNewBookResponse>>
{
    private readonly LibraryManagementDbContext _context;
    private readonly IStorageService _storageService;
    private readonly ILogger<AddNewBookHandler> _logger;

    public AddNewBookHandler(LibraryManagementDbContext context, IStorageService storageService,
        ILogger<AddNewBookHandler> logger)
    {
        _context = context;
        _storageService = storageService;
        _logger = logger;
    }

    public async Task<BaseResponse<AddNewBookResponse>> Handle(AddNewBookRequest request, CancellationToken cancellationToken)
    {
        // Upload the book image to the specified folder using the storage service
        string imageUrl = await _storageService.UploadFileAsync(request.Image, Paths.BookFolder);

        // Check if the image upload was successful
        if (imageUrl is null)
            throw new CustomException(ErrorHandling.ErrorType.ImageUploadError);

        // Create a new book entity with the provided information
        var book = new Domain.Entities.Book
        {
            Name = request.Name,
            AuthorName = request.AuthorName,
            PublisherName = request.PublisherName,
            PageCount = request.PageCount,
            Category = request.Category,
            PublicationDate = request.PublicationDate,
            Stock = request.Stock,
            LoanedCount = request.LoanedCount,
            ImageUrl = imageUrl
        };

        // Add the book to the database context
        await _context.Books.AddAsync(book, cancellationToken);

        // Save the changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        // Log the successful addition of the book
        _logger.LogInformation($"{request.Name} added successfully.");

        // Return a base response indicating the success of the operation
        return new BaseResponse<AddNewBookResponse>
        {
            Message = "Book added successfully."
        };
    }
}
