using LibraryManagement.Core.Exception;

namespace LibraryManagement.Core.Response
{
    // Represents the base response class that defines the basic structure of API responses.
    public class BaseResponse<T>
    {
        // Specifies the status of the response, which can be "Success" or "Error".
        public string Status { get; set; } = ErrorHandling.Success;

        // Holds a list of errors.
        public List<ErrorResponse> Errors { get; set; }

        // An optional message that can be included.
        public string? Message { get; set; }

        // The response data.
        public T? Data { get; set; }
    }
}
