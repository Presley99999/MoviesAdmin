using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace MoviesAdmin.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureStorageService(IConfiguration config)
        {
            _containerName = config["AzureStorage:ContainerName"];
            var connectionString = config["AzureStorage:ConnectionString"];

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, options);

            return blob.Uri.ToString();
        }
    }
}
