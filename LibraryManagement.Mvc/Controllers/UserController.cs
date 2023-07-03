using LibraryManagement.Business.Features.User.Commands.CreateUser;
using LibraryManagement.Business.Features.User.Commands.UpdateUserById;
using LibraryManagement.Business.Features.User.Queries.GetUserById;
using LibraryManagement.Business.Features.User.Queries.GetUsers;
using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using LibraryManagement.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// Importing the necessary namespaces.

namespace LibraryManagement.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        // A private field to store the IMediator instance.

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
            // Injecting the IMediator instance through the constructor.
        }

        public IActionResult Index()
        {
            return View();
            // The Index action method that returns a view.
        }

        [HttpPost]
        public async Task<GetUsersResponse> GetUsers(GetUsersRequest request)
        {
            return await _mediator.Send(request);
            // An action method to handle the HTTP POST request for retrieving users.
            // It sends the request to the mediator and awaits the response of type GetUsersResponse.
        }

        public IActionResult Create()
        {
            return View();
            // An action method to handle the HTTP GET request for creating a new user.
            // It returns the create user view.
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserUpdateOrCreateModel model)
        {
            CreateUserRequest request = new()
            {
                FullName = model.FullName,
                Email = model.Email,
                Gsm = model.Gsm,
                Address = model.Address
            };

            BaseResponse<CreateUserResponse> response = await _mediator.Send(request);

            if (response.Status == ErrorHandling.Success)
            {
                ViewBag.SuccessMessage = response.Message!;
            }
            else
                ViewBag.ErrorMessage = response.Errors;

            return View(model);
            // An action method to handle the HTTP POST request for creating a new user.
            // It creates a CreateUserRequest using the data from the model and sends it to the mediator.
            // It awaits the response of type BaseResponse<CreateUserResponse>.
            // If the response is successful, it sets a success message to ViewBag. Otherwise, it sets an error message.
            // Finally, it returns the create user view with the model.
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            GetUserByIdRequest request = new()
            {
                Id = id
            };

            BaseResponse<GetUserByIdResponse> response = await _mediator.Send(request);

            UserUpdateOrCreateModel userUpdateOrCreateModel = new()
            {
                Id = response.Data.Id,
                FullName = response.Data.FullName,
                Email = response.Data.Email,
                Gsm = response.Data.Gsm,
                Address = response.Data.Address
            };

            return View(userUpdateOrCreateModel);
            // An action method to handle the HTTP GET request for editing a user.
            // It creates a GetUserByIdRequest using the provided id and sends it to the mediator.
            // It awaits the response of type BaseResponse<GetUserByIdResponse>.
            // It creates a UserUpdateOrCreateModel using the data from the response.
            // Finally, it returns the edit user view with the model.
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateOrCreateModel model)
        {
            UpdateUserByIdRequest request = new()
            {
                Id = (Guid)model.Id!,
                FullName = model.FullName,
                Email = model.Email,
                Gsm = model.Gsm,
                Address = model.Address
            };

            var response = await _mediator.Send(request);

            if (response.Status == ErrorHandling.Success)
            {
                ViewBag.SuccessMessage = response.Message!;
            }
            else
                ViewBag.ErrorMessage = response.Errors;

            return View(model);
            // An action method to handle the HTTP POST request for editing a user.
            // It creates an UpdateUserByIdRequest using the data from the model and sends it to the mediator.
            // It awaits the response and checks if it is successful.
            // If the response is successful, it sets a success message to ViewBag. Otherwise, it sets an error message.
            // Finally, it returns the edit user view with the model.
        }
    }
}
