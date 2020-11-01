using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzBlazorApp.Data.Services.Interfaces
{
    public interface IBlobStorageService
    {
        BlobServiceClient GetBlobServiceClient(string connString);
        Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName);
        BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string containerName);
        IList<BlobItem> GetAllBlobsInContainer(BlobContainerClient blobContainerClient);
        IList<BlobContainerItem> GetAllContainers(BlobServiceClient blobServiceClient);
        void UploadContentToContainer(BlobContainerClient blobContainerClient, string localFilePath, string uploadMode);
        Task CreateContainer(BlobServiceClient blobServiceClient, string containerName);
        Task DeleteContainer(BlobServiceClient blobServiceClient, string containerName);
    }
}
