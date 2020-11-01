using AzBlazorApp.Data.Services;
using FluentAssertions;
using Xunit;

namespace AzBlazorApp.Test.Services
{
    public class BlobStorageServiceTests : AbstractServiceTest<BlobStorageService>
    {
        [Fact]
        public void CanGetBlobServiceClient() => RunOnService(service =>
                                               {
                                                   var blobServiceClient = service.GetBlobServiceClient(ConnString);
                                                   blobServiceClient.Should().NotBeNull();
                                               });

        protected override BlobStorageService NewService() => new BlobStorageService(GetLoggerFake<BlobStorageService>());
    }
}
