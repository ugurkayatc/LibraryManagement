using FluentValidation.Results;
using LibraryManagement.Core.Response;

namespace LibraryManagement.Core.Exception
{
    public class BadRequestException : System.Exception
    {
        /// <summary>
        /// Gets the list of error responses associated with the exception.
        /// </summary>
        public List<ErrorResponse> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a list of error responses.
        /// </summary>
        /// <param name="errors">The list of error responses.</param>
        public BadRequestException(List<ErrorResponse> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a list of validation failures.
        /// </summary>
        /// <param name="errors">The list of validation failures.</param>
        public BadRequestException(List<ValidationFailure> errors)
        {
            Errors = new List<ErrorResponse>();
            foreach (var error in errors)
            {
                Errors.Add(new ErrorResponse
                {
                    Key = error.PropertyName,
                    Description = error.ErrorMessage
                });
            }
        }
    }
}
