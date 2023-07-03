using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Business.Features.LentBook.Commands.CreateLentBook
{
    // Represents the handler for creating a lent book
    public class CreateLentBookHandler : IRequestHandler<CreateLentBookRequest, BaseResponse<CreateLentBookResponse>>
    {
        private readonly LibraryManagementDbContext _context;
        private readonly ILogger<CreateLentBookHandler> _logger;

        public CreateLentBookHandler(LibraryManagementDbContext context, ILogger<CreateLentBookHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<CreateLentBookResponse>> Handle(CreateLentBookRequest request, CancellationToken cancellationToken)
        {
            // Begin a database transaction
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // Retrieve the book from the database
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken: cancellationToken);

                // Create a new lent book entry
                await _context.LentBooks.AddAsync(new Domain.Entities.LentBook
                {
                    UserId = request.UserId,
                    BookId = request.BookId,
                    ReturnDate = request.ReturnDate
                }, cancellationToken);

                // Increment the loaned count of the book
                book.LoanedCount += 1;

                // Save changes to the database
                await _context.SaveChangesAsync(cancellationToken);

                // Commit the transaction
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                // Rollback the transaction in case of an exception
                await transaction.RollbackAsync(cancellationToken);
            }

            // Log the successful lent book operation
            _logger.LogInformation($"{request.UserId} lent {request.BookId} Book successfully.");

            // Return a response indicating the success of the lent book operation
            return new BaseResponse<CreateLentBookResponse>
            {
                Message = "Lent book successfully.",
            };
        }
    }
}
