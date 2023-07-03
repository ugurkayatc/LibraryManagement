using LibraryManagement.Core.Response;

namespace LibraryManagement.Core.Exception
{
    public class CustomException : System.Exception
    {
        /// <summary>
        /// Gets the list of error responses associated with the exception.
        /// </summary>
        public List<ErrorResponse> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with a list of error responses.
        /// </summary>
        /// <param name="errors">The list of error responses.</param>
        public CustomException(List<ErrorResponse> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with an error type.
        /// </summary>
        /// <param name="errorType">The error type.</param>
        public CustomException(ErrorHandling.ErrorType errorType)
        {
            Errors = new List<ErrorResponse>();
            Errors.Add(errorType);
        }
    }
}
