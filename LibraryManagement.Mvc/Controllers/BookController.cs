using LibraryManagement.Business.Features.Book.Commands.AddNewBook;
using LibraryManagement.Business.Features.Book.Commands.UpdateBookById;
using LibraryManagement.Business.Features.Book.Queries.GetAllBook;
using LibraryManagement.Business.Features.Book.Queries.GetBookById;
using LibraryManagement.Core;
using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// Importing the necessary namespaces.

namespace LibraryManagement.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        // A private field to store the IMediator instance.

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
            // Injecting the IMediator instance through the constructor.
        }

        public IActionResult Index(GetAllBookRequest getAllBookRequest)
        {
            return View();
            // The Index action method that returns a View.
        }

        [HttpPost]
        public async Task<GetAllBookResponse> GetBooks(GetAllBookRequest request)
        {
            return await _mediator.Send(request);
            // An action method to handle the HTTP POST request for retrieving all books.
            // It sends the request to the mediator and awaits the response.
        }

        [HttpGet]
        public async Task<IActionResult> Edit(GetBookByIdRequest request)
        {
            BaseResponse<GetBookByIdResponse> response = await _mediator.Send(request);
            // An action method to handle the HTTP GET request for editing a book.
            // It sends the request to the mediator and awaits the response.

            // Mapping the response data to the BookUpdateOrCreateModel.
            BookUpdateOrCreateModel bookUpdateOrCreateModel = new BookUpdateOrCreateModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                AuthorName = response.Data.AuthorName,
                PublisherName = response.Data.PublisherName,
                PageCount = response.Data.PageCount,
                Category = response.Data.Category,
                PublicationDate = response.Data.PublicationDate,
                Stock = response.Data.Stock,
                LoanedCount = response.Data.LoanedCount,
                ImageUrl = response.Data.ImageUrl
            };

            return View(bookUpdateOrCreateModel);
            // Returning the view with the BookUpdateOrCreateModel.
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateOrCreateModel model)
        {
            UpdateBookByIdRequest request = new UpdateBookByIdRequest
            {
                Id = (Guid)model.Id,
                Name = model.Name,
                AuthorName = model.AuthorName,
                PublisherName = model.PublisherName,
                PageCount = model.PageCount,
                Category = model.Category,
                PublicationDate = model.PublicationDate,
                Stock = model.Stock,
                LoanedCount = model.LoanedCount,
                Image = model.Image
            };

            BaseResponse<UpdateBookByIdResponse> response = await _mediator.Send(request);
            // An action method to handle the HTTP POST request for updating a book.
            // It creates an UpdateBookByIdRequest from the model and sends the request to the mediator.

            if (response.Status == ErrorHandling.Success)
            {
                ViewBag.SuccessMessage = response.Message;
                model.ImageUrl = Paths.rootUrl + response.Data.ImageUrl;
            }
            else
                return ViewBag.ErrorMessage = response.Errors;

            return View(model);
            // Returning the view with the updated model.
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
            // An action method to handle the HTTP GET request for creating a new book.
            // It returns the create view.
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookUpdateOrCreateModel model)
        {
            AddNewBookRequest request = new()
            {
                Name = model.Name,
                AuthorName = model.AuthorName,
                PublisherName = model.PublisherName,
                PageCount = model.PageCount,
                Category = model.Category,
                PublicationDate = model.PublicationDate,
                Stock = model.Stock,
                LoanedCount = model.LoanedCount,
                Image = model.Image
            };

            BaseResponse<AddNewBookResponse> response = await _mediator.Send(request);
            // An action method to handle the HTTP POST request for adding a new book.
            // It creates an AddNewBookRequest from the model and sends the request to the mediator.

            if (response.Status == ErrorHandling.Success)
            {
                ViewBag.SuccessMessage = response.Message!;
            }
            else
                return ViewBag.ErrorMessage = response.Errors;

            return View(model);
            // Returning the view with the model.
        }
    }
}
