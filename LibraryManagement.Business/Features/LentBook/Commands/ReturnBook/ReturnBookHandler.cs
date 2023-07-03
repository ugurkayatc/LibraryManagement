using LibraryManagement.Core.Response;
using LibraryManagement.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Business.Features.LentBook.Commands.ReturnBook
{
    // Handler for returning a lent book
    public class ReturnBookHandler : IRequestHandler<ReturnBookRequest, BaseResponse<ReturnBookResponse>>
    {
        private readonly LibraryManagementDbContext _context;
        private readonly ILogger<ReturnBookHandler> _logger;

        public ReturnBookHandler(LibraryManagementDbContext context, ILogger<ReturnBookHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Handles the request to return a lent book
        public async Task<BaseResponse<ReturnBookResponse>> Handle(ReturnBookRequest request,
            CancellationToken cancellationToken)
        {
            // Find the lent book by ID and include the related book entity
            Domain.Entities.LentBook? lentBook = await _context.LentBooks.Where(x => x.Id == request.Id)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            // Decrement the loaned count of the book
            lentBook.Book.LoanedCount -= 1;

            // Update the lent book properties
            lentBook.IsReturned = true;
            lentBook.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{request.Id} returned Book successfully.");

            return new BaseResponse<ReturnBookResponse>
            {
                Message = "Book returned successfully.",
            };
        }
    }
}
