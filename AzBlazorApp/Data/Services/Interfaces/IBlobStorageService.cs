using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzBlazorApp.Data.Services.Interfaces
{
    interface IBlobStorageService
    {
        BlobServiceClient GetBlobStorageClient(string connString);
        IList<BlobItem> GetAllBlobs();
        IList<BlobContainerItem> GetAllContainers();
        Task UploadContentToContainer(string localFilePath);
        Task CreateContainer(string name, BlobServiceClient client);
        Task DeleteContainer(string name, BlobServiceClient client);
    }
}
