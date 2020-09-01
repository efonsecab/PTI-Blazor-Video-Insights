using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PTIBlazorVideoInsightsCourse.Shared.Models;

namespace PTIBlazorVideoInsightsCourse.Client.Pages
{
    public partial class Keywords
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        List<KeywordInfoModel> KeywordsInfoResult { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.KeywordsInfoResult =
                await this.httpClient.
                GetFromJsonAsync<List<KeywordInfoModel>>("VideoIndexer/GetAllKeywords");
        }
    }
}
