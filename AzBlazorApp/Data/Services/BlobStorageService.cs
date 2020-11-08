using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private BlobContentInfo response;

        public BlobStorageService(ILogger<BlobStorageService> logger) => this.logger = logger;

        public Task CreateContainer(BlobServiceClient blobServiceClient, string containerName) =>
            blobServiceClient.CreateBlobContainerAsync(containerName);

        public Task DeleteContainer(BlobServiceClient blobServiceClient, string containerName) =>
            blobServiceClient.DeleteBlobContainerAsync(containerName);

        public IList<BlobContainerItem> GetAllContainers(BlobServiceClient blobServiceClient) =>
            (IList<BlobContainerItem>)blobServiceClient.GetBlobContainersAsync();

        public IList<BlobItem> GetAllBlobsInContainer(BlobContainerClient blobContainerClient) =>
            (IList<BlobItem>)blobContainerClient.GetBlobsAsync();

        public BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string containerName)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(containerName);
            return blobClient;
        }

        public BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return blobContainerClient;
        }

        public BlobServiceClient GetBlobServiceClient(string connString)
        {
            var client = new BlobServiceClient(connString);
            return client;
        }

        public async Task<BlobContentInfo> UploadContentToContainer(BlobContainerClient blobContainerClient, string localPath, string uploadMode)
        {
            string blobName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                blobName = localPath.Split('\\').Last();
            }
            else
            {
                blobName = localPath.Split('/').Last();
            }

            BlobClient blobClient = GetBlobClient(blobContainerClient, blobName);

            if (File.Exists(localPath))
            {
                response = await UploadLocalFileToBlobStorage(localPath, blobClient);
            }
            else if (Directory.Exists(localPath))
            {
                await ProcessLocalDirectory(localPath, uploadMode, blobClient);
            }
            return response;
        }

        public async Task ProcessLocalDirectory(string targetDirectory, string uploadMode, BlobClient blobClient)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                await UploadLocalFileToBlobStorage(fileName, blobClient);

            if (uploadMode == "Recursive")
            {
                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    await ProcessLocalDirectory(subdirectory, uploadMode, blobClient);
            }
        }

        public async Task<BlobContentInfo> UploadLocalFileToBlobStorage(string localPath, BlobClient blobClient)
        {
            using FileStream fileStream = File.OpenRead(localPath);
            return await blobClient.UploadAsync(fileStream, true);

            // ("Processed file '{0}'.", path);
        }

        Task IBlobStorageService.UploadContentToContainer(BlobContainerClient blobContainerClient, string localFilePath, string uploadMode)
        {
            throw new System.NotImplementedException();
        }
    }
}
