using System;
using System.Collections.Generic;
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
                await Task.Yield();
                AzureVideoIndexerHelper helper = new AzureVideoIndexerHelper(this.AzureConfiguration,
                    this.HttpClientFactory.CreateClient());
                var taskGetAllPersonsData = helper.GetAllPersonsData();
                var taskGetAllKeywordsAction = helper.GetAllKeywords();
                Task.WaitAll(new Task[] {taskGetAllPersonsData, taskGetAllKeywordsAction });
                this.MemoryCache.Set<GetAllPersonsModel>(Constants.ALLPERSONS_INFO, taskGetAllPersonsData.Result);
                this.MemoryCache.Set<List<KeywordInfoModel>>(Constants.ALLVIDEOS_KEYWORDS, taskGetAllKeywordsAction.Result);
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}