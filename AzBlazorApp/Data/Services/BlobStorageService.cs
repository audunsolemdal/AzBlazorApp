using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzBlazorApp.Data.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace AzBlazorApp.Data.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly ILogger<BlobStorageService> logger;

        public BlobStorageService(ILogger<BlobStorageService> logger) => this.logger = logger;

        public Task CreateContainer(BlobServiceClient blobServiceClient, string containerName)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteContainer(BlobServiceClient blobServiceClient, string containerName)
        {
            throw new System.NotImplementedException();
        }

        public IList<BlobItem> GetAllBlobsInContainer(BlobContainerClient blobContainerClient)
        {
            throw new System.NotImplementedException();
        }

        public IList<BlobContainerItem> GetAllContainers(BlobServiceClient blobServiceClient)
        {
            throw new System.NotImplementedException();
        }

        public BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string containerName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            var blobContainerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            return blobContainerClient;
        }

        public BlobServiceClient GetBlobServiceClient(string connString)
        {
            var client = new BlobServiceClient(connString);
            return client;
        }

        public void UploadContentToContainer(BlobContainerClient blobContainerClient, string localPath, string uploadMode)
        {
            BlobClient blobClient = GetBlobClient(blobContainerClient, "myContainer");

            if (File.Exists(localPath))
            {
                UploadBlob(localPath, blobClient);
            }
            else if (Directory.Exists(localPath))
            {
                ProcessDirectory(localPath, uploadMode, blobClient);
            }
        }

        public void ProcessDirectory(string targetDirectory, string uploadMode, BlobClient blobClient)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                UploadBlob(fileName, blobClient);

            if (uploadMode == "Recursive")
            {
                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, uploadMode, blobClient);
            }
        }

        public static void UploadBlob(string localPath, BlobClient blobClient)
        {
            using (FileStream uploadBlobStream = File.OpenRead(localPath))
            {
                blobClient.UploadAsync(uploadBlobStream, true);
            }

            // ("Processed file '{0}'.", path);
        }
    }
}
