namespace LibraryManagement.Mvc.Models
{
    public class ErrorViewModel
    {
        // The ID of the request that caused the error (nullable)
        public string? RequestId { get; set; }

        // Determines whether to show the request ID or not
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
