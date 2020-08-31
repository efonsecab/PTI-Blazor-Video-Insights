using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using PTIBlazorVideoInsightsCourse.Shared;
using PTIBlazorVideoInsightsCourse.Shared.Helpers;
using PTIBlazorVideoInsightsCourse.Shared.Models;

namespace PTIBlazorVideoInsightsCourse.Server
{
    internal class IndexedPersonsService : BackgroundService
    {
        public IndexedPersonsService(AzureConfiguration azureConfiguration,
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache):base()
        {
            this.AzureConfiguration = azureConfiguration;
            this.HttpClientFactory = httpClientFactory;
            this.MemoryCache = memoryCache;
        }

        public AzureConfiguration AzureConfiguration { get; }
        public IHttpClientFactory HttpClientFactory { get; }
        public IMemoryCache MemoryCache { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                AzureVideoIndexerHelper helper = new AzureVideoIndexerHelper(this.AzureConfiguration,
                    this.HttpClientFactory.CreateClient());
                var allPersonsInfo = await helper.GetAllPersonsData();
                this.MemoryCache.Set<GetAllPersonsModel>(Constants.ALLPERSONS_INFO, allPersonsInfo);
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}