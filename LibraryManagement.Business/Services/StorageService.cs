using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using LibraryManagement.Core.Exception;
using LibraryManagement.Domain.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Business.Services
{
    // Represents a service for file storage.
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;

        // Initializes a new instance of the StorageService class.
        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Uploads a file to the specified path in the storage.
        public async Task<string?> UploadFileAsync(IFormFile file, string path)
        {
            CheckFileExtension(file);
            var client = new AmazonS3Client(_configuration["AWS:AccessKey"], _configuration["AWS:SecretKey"], RegionEndpoint.EUCentral1);

            string fileUrl = $"{path}/{Guid.NewGuid() + Path.GetExtension(file.FileName)}";

            PutObjectResponse? response = await client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _configuration["AWS:BucketName"],
                Key = fileUrl,
                InputStream = file.OpenReadStream(),
            });

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK ? fileUrl : null;
        }

        // Checks the file extension to ensure it is allowed.
        private void CheckFileExtension(IFormFile file)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".json" };

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                throw new CustomException(ErrorHandling.ErrorType.FileExtensionError);
        }
    }
}
