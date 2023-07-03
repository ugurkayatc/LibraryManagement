using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Domain.Abstractions.Storage
{
    public interface IStorageService
    {
        // Uploads a file to the specified path
        // Returns the URL of the uploaded file
        Task<string?> UploadFileAsync(IFormFile file, string path);
    }
}
