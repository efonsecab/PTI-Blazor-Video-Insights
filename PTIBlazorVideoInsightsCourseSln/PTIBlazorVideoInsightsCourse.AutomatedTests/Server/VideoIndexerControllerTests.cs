using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTIBlazorVideoInsightsCourse.Server;
using PTIBlazorVideoInsightsCourse.Server.Controllers;
using PTIBlazorVideoInsightsCourse.Shared;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.ListVideos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        private IHttpClientFactory HttpClientFactory { get; }
        private HttpClient ServerClient { get; }

        private readonly TestServer Server;
        private readonly ServiceCollection Services;

        public VideoIndexerControllerTests()
        {
            ConfigurationBuilder configurationBuilder =
                new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json")
                .AddUserSecrets("5ee6af21-ee0f-4995-b5aa-ae4a9aafda1d");
            IConfiguration configuration = configurationBuilder.Build();
            var azureConfiguration = configuration.GetSection("AzureConfiguration").Get<AzureConfiguration>();
            this.AzureConfiguration = azureConfiguration;
            Server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>());
            this.Services = new ServiceCollection();
            this.Services.AddHttpClient("VideoIndexerAnonymousApiClient");
            this.Services.AddHttpClient("VideoIndexerAuthorizedApiClient", configuration =>
            {
                configuration.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    azureConfiguration.VideoIndexerConfiguration.SubscriptionKey);
            });
            this.HttpClientFactory = this.Services.BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>();
            this.ServerClient = this.Server.CreateClient();
        }

        [TestInitialize]
        public void InitializeTests()
        {
        }

        [TestMethod]
        public async Task Test_ListVideosAsync()
        {
            var result = await this.ServerClient
                .GetFromJsonAsync<ListVideosResponse>("/VideoIndexer/ListVideos");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Test_GetAccountAccessTokenAsync()
        {
            var result = await this.ServerClient
                .GetStringAsync("/VideoIndexer/GetAccountAccessToken");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Test_GetVideoAccessTokenAsync()
        {
            var listVideosResult = await this.ServerClient
                .GetFromJsonAsync<ListVideosResponse>("/VideoIndexer/ListVideos");
            Assert.IsNotNull(listVideosResult);
            var firstVideo = listVideosResult.results.First();
            var result = await this.ServerClient
                .GetStringAsync($"/VideoIndexer/GetVideoAccessToken" +
                $"?videoId={firstVideo.id}&allowEdit={true}");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Test_GetVideoThumbnailAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration, this.HttpClientFactory);
            OkObjectResult videosListResult = (OkObjectResult)await controller.ListVideos();
            var videosList =
                (LV.ListVideosResponse)videosListResult.Value;
            var firstVideo = videosList.results.First();
            var result = await controller.GetVideoThumbnail(videoId: firstVideo.id, thumbnailId: firstVideo.thumbnailId);
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public void Test_GetLocation()
        {
            VideoIndexerController controller = new VideoIndexerController(this.AzureConfiguration,
                this.HttpClientFactory);
            var result = controller.GetLocation();
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }

        [TestMethod]
        public async Task Test_SearchVideosAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration, this.HttpClientFactory);
            var result = await controller.SearchVideos(keyword: "blazor");
            Assert.IsTrue(result is OkObjectResult, "Invalid result");
        }
    }
}
