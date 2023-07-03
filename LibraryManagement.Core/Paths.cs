namespace LibraryManagement.Core
{
    // Represents a class that contains paths used for AWS S3 file uploads and database storage.
    public static class Paths
    {
        // The root URL of the AWS S3 bucket.
        public const string rootUrl = "https://d2an46wc102jd8.cloudfront.net/";

        // The base folder for file storage.
        private const string baseFolder = "files/";

        // The folder path for storing book files.
        public const string BookFolder = baseFolder + "book";
    }
}
