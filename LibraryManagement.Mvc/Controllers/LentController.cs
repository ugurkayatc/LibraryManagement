using LibraryManagement.Business.Features.LentBook.Commands.CreateLentBook;
using LibraryManagement.Business.Features.LentBook.Commands.ReturnBook;
using LibraryManagement.Business.Features.LentBook.Queries.GetLentBooks;
using LibraryManagement.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// Importing the necessary namespaces.

namespace LibraryManagement.Mvc.Controllers
{
    public class LentController : Controller
    {
        private readonly IMediator _mediator;
        // A private field to store the IMediator instance.

        public LentController(IMediator mediator)
        {
            _mediator = mediator;
            // Injecting the IMediator instance through the constructor.
        }

        public IActionResult Index()
        {
            return View();
            // The Index action method that returns a View.
        }

        public IActionResult LentBook()
        {
            return View();
            // An action method to handle the HTTP GET request for lending a book.
            // It returns the lend book view.
        }

        [HttpPost]
        public async Task<BaseResponse<CreateLentBookResponse>> LentBook(CreateLentBookRequest request)
        {
            return await _mediator.Send(request);
            // An action method to handle the HTTP POST request for lending a book.
            // It sends the request to the mediator and awaits the response of type CreateLentBookResponse.
        }

        [HttpPost]
        public async Task<GetLentBooksResponse> GetLentBooks(GetLentBooksRequest request)
        {
            return await _mediator.Send(request);
            // An action method to handle the HTTP POST request for retrieving lent books.
            // It sends the request to the mediator and awaits the response of type GetLentBooksResponse.
        }

        [HttpPost]
        public async Task<BaseResponse<ReturnBookResponse>> ReturnBook(ReturnBookRequest request)
        {
            return await _mediator.Send(request);
            // An action method to handle the HTTP POST request for returning a book.
            // It sends the request to the mediator and awaits the response of type ReturnBookResponse.
        }
    }
}
