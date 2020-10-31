using AzBlazorApp.Data.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzBlazorApp.Data.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        public Task CreateContainer(string name, BlobServiceClient client)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteContainer(string name, BlobServiceClient client)
        {
            throw new System.NotImplementedException();
        }

        public IList<BlobItem> GetAllBlobs()
        {
            throw new System.NotImplementedException();
        }

        public IList<BlobContainerItem> GetAllContainers()
        {
            throw new System.NotImplementedException();
        }

        public BlobServiceClient GetBlobStorageClient(string connString)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadContentToContainer(string localFilePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
