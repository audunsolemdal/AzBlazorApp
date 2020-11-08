using System;
using AzBlazorApp.Data.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzBlazorApp.Test.Services
{
    public class BlobStorageServiceTests : AbstractServiceTest<BlobStorageService>
    {
        private static readonly ILogger<BlobStorageService> Logger = GetLoggerFake<BlobStorageService>();

        [Fact]
        public void CanGetBlobServiceClient() => RunOnService(service =>
                                               {
                                                   BlobServiceClient blobServiceClient = service.GetBlobServiceClient(ConnString);
                                                   _ = blobServiceClient.Should().NotBeNull();
                                               });

        [Fact]
        public void CanDeleteContainer() => RunOnService(service =>
        {
            BlobServiceClient cli = GetBlobServiceClient();

            try
            {
                _ = cli.DeleteBlobContainerAsync("dummycontainer");
            }
            catch
            {
                throw new Exception();
            }
        });

        [Fact]
        public void CanCreateContainer() => RunOnService(service =>
        {
        BlobServiceClient cli = GetBlobServiceClient();

        try
            {
                _ = cli.CreateBlobContainerAsync("dummycontainer");
            }
        catch
            {
                throw new Exception();
            }
        });

        [Fact]
        public void CanGetAllBlobsInContainer() => RunOnService(service =>
        {
            BlobContainerClient blobContainerClient = GetBlobContainerClient("dummycontainer");
            _ = blobContainerClient.GetBlobsAsync();
        });

        [Fact]
        public void CanUploadBlobsRecursivelyToContainer() => RunOnService(async service =>
        {
            BlobContainerClient blobContainerClient = GetBlobContainerClient("dummycontainer");
            BlobContentInfo response = await service.UploadContentToContainer(blobContainerClient, "C:\\dummy.txt", "Recursive");
        });

        [Fact]
        public void CanGetBlobContainerClient() => RunOnService(service =>
        {
            BlobContainerClient blobContainerClient = GetBlobContainerClient("dummycontainer");
            _ = blobContainerClient.Should().NotBeNull();
        });

        public BlobServiceClient GetBlobServiceClient()
        {
            BlobStorageService serv = NewService();
            BlobServiceClient client = serv.GetBlobServiceClient(ConnString);
            return client;
        }

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            BlobStorageService serv = NewService();
            BlobServiceClient client1 = serv.GetBlobServiceClient(ConnString);
            BlobContainerClient client2 = client1.GetBlobContainerClient(containerName);
            return client2;
        }

        protected override BlobStorageService NewService() =>
        new BlobStorageService(GetLoggerFake<BlobStorageService>());
    }
}
