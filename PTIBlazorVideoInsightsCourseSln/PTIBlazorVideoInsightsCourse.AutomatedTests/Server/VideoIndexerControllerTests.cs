using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTIBlazorVideoInsightsCourse.Server.Controllers;
using PTIBlazorVideoInsightsCourse.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
