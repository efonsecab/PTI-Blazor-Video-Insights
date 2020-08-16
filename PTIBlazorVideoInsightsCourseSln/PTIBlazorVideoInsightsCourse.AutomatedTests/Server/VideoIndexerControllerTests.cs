using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTIBlazorVideoInsightsCourse.Server.Controllers;
using PTIBlazorVideoInsightsCourse.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LV = PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.ListVideos;

namespace PTIBlazorVideoInsightsCourse.AutomatedTests.Server
{
    [TestClass]
    public class VideoIndexerControllerTests
    {
        private AzureConfiguration AzureConfiguration { get; set; }
        [TestInitialize]
        public void InitializeTests()
        {
            ConfigurationBuilder configurationBuilder =
                new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json")
                .AddUserSecrets("5ee6af21-ee0f-4995-b5aa-ae4a9aafda1d");
            IConfiguration configuration = configurationBuilder.Build();
            var azureConfiguration = configuration.GetSection("AzureConfiguration").Get<AzureConfiguration>();
            this.AzureConfiguration = azureConfiguration;
        }

        [TestMethod]
        public async Task ListVideosTestAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            var result = await controller.ListVideos();
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public async Task GetAccountAccessTokenAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            var result = await controller.GetAccountAccessToken(false);
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public async Task GetVideoAccessTokenAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            OkObjectResult videosListResult = (OkObjectResult)await controller.ListVideos();
            var videosList =
                (LV.ListVideosResponse)videosListResult.Value;
            var firstVideo = videosList.results.First();
            var result = await controller.GetVideoAccessToken(firstVideo.id, false);
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public async Task GetVideoThumbnailAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            OkObjectResult videosListResult = (OkObjectResult)await controller.ListVideos();
            var videosList =
                (LV.ListVideosResponse)videosListResult.Value;
            var firstVideo = videosList.results.First();
            var result = await controller.GetVideoThumbnail(videoId: firstVideo.id, thumbnailId: firstVideo.thumbnailId);
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public void GetLocation()
        {
            VideoIndexerController controller = new VideoIndexerController(this.AzureConfiguration);
            var result = controller.GetLocation();
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }
    }
}
