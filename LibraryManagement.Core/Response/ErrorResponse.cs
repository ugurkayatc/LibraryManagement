namespace LibraryManagement.Core.Response
{
    // Represents an error response object.
    public class ErrorResponse
    {
        // The key or identifier associated with the error.
        public string? Key { get; set; }

        // The error code.
        public int Code { get; set; }

        // A description or message that provides more information about the error.
        public string Description { get; set; }
    }
}
