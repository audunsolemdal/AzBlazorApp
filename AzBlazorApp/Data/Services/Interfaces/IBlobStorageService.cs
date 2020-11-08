using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzBlazorApp.Data.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task CreateContainer(BlobServiceClient blobServiceClient, string containerName);
        Task DeleteContainer(BlobServiceClient blobServiceClient, string containerName);
        IList<BlobItem> GetAllBlobsInContainer(BlobContainerClient blobContainerClient);
        IList<BlobContainerItem> GetAllContainers(BlobServiceClient blobServiceClient);
        BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string containerName);
        BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName);
        BlobServiceClient GetBlobServiceClient(string connString);
        Task UploadContentToContainer(BlobContainerClient blobContainerClient, string localFilePath, string uploadMode);
        Task ProcessLocalDirectory(string targetDirectory, string uploadMode, BlobClient blobClient);
        Task<BlobContentInfo> UploadLocalFileToBlobStorage(string localPath, BlobClient blobClient);
    }
}
