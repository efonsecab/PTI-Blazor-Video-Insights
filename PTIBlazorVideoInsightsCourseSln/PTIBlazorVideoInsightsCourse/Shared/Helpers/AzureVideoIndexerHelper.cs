using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PTIBlazorVideoInsightsCourse.Shared.Models;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersonModels;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersons;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetVideoIndex;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.ListVideos;

namespace PTIBlazorVideoInsightsCourse.Shared.Helpers
{
    public class AzureVideoIndexerHelper
    {
        public AzureConfiguration AzureConfiguration { get; }
        public HttpClient HttpClient { get; }

        public AzureVideoIndexerHelper(AzureConfiguration azureConfiguration,
            HttpClient httpClient)
        {
            this.AzureConfiguration = azureConfiguration;
            this.HttpClient = httpClient;
        }

        public async Task<string> GetVideoAccessTokenStringAsync(string videoId, bool allowEdit)
        {
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
                $"/Auth/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos/{videoId}/AccessToken" +
                $"?allowEdit={allowEdit}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                this.AzureConfiguration.VideoIndexerConfiguration.SubscriptionKey);
            var result = await client.GetStringAsync(requestUrl);
            return result.Replace("\"", string.Empty);
        }

        public async Task<GetVideoIndexResponse> GetVideoIndex(Shared.Models.AzureVideoIndexer.ListVideos.Result singleVideo)
        {
            string videoAccessToken = await this.GetVideoAccessTokenStringAsync(singleVideo.id, true);
            string requestUrl = $"https://api.videoindexer.ai" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos/{singleVideo.id}" +
                $"/Index" +
                $"?accessToken={videoAccessToken}";
            var videoIndexResult = await this.HttpClient.GetFromJsonAsync<GetVideoIndexResponse>(requestUrl);
            return videoIndexResult;
        }

        public async Task<ListVideosResponse> GetAllVideos()
        {
            var accountAccessToken =
                await this.GetAccountAccessTokenString(false);
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos?accessToken={accountAccessToken}";
            var result = await this.HttpClient.GetFromJsonAsync<ListVideosResponse>(requestUrl);
            return result;
        }

        public async Task<List<KeywordInfoModel>> GetAllKeywords()
        {
            List<KeywordInfoModel> lstKeywords = new List<KeywordInfoModel>();
            var allVideos = await this.GetAllVideos();
            foreach (var singleVideo in allVideos.results)
            {
                var singleVideoIndex = await this.GetVideoIndex(singleVideo);
                if (singleVideoIndex.summarizedInsights.keywords.Count() > 0)
                {
                    foreach (var singleKeyword in singleVideoIndex.summarizedInsights.keywords)
                    {
                        var existentKeyWordInfo = lstKeywords.Where(p => p.Keyword == singleKeyword.name).SingleOrDefault();
                        if (existentKeyWordInfo != null)
                        {
                            existentKeyWordInfo.Appeareances += singleKeyword.appearances.Count();
                        }
                        else
                        {
                            existentKeyWordInfo = new KeywordInfoModel()
                            {
                                Keyword = singleKeyword.name,
                                Appeareances = singleKeyword.appearances.Count()
                            };
                            lstKeywords.Add(existentKeyWordInfo);
                        }
                    }
                }
            }
            return lstKeywords.Distinct().ToList();
        }

        public async Task<GetAllPersonsModel> GetAllPersonsData()
        {
            GetAllPersonsModel model = new GetAllPersonsModel()
            {
                Persons = new System.Collections.Generic.List<GettAllPersonsModelItem>()
            };
            var personsModels = await this.GetAllPersonsModels();
            foreach (var singlePersonModel in personsModels)
            {
                var personsInModel = await this.GetAllPersonsInPersonModel(singlePersonModel.id);
                foreach (var singlePersonInModel in personsInModel.results)
                {
                    var faceBase64String = await this.GetPersonPictureInBase64(singlePersonModel.id,
                        singlePersonInModel.id, singlePersonInModel.sampleFace.id);
                    model.Persons.Add(new GettAllPersonsModelItem()
                    {
                        PersonName = singlePersonInModel.name,
                        PersonPictureBase64 = faceBase64String
                    }
                    );
                }
            }

            return model;
        }

        public async Task<string> GetPersonPictureInBase64(string personModelId, string personId, string faceId)
        {
            string accountAccessToken = await this.GetAccountAccessTokenString(false);
            string requestUrl = $"https://api.videoindexer.ai/" +
                $"{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Customization" +
                $"/PersonModels/{personModelId}" +
                $"/Persons/{personId}" +
                $"/Faces/{faceId}" +
                $"?accessToken={accountAccessToken}";
            var imageBytes = await this.HttpClient.GetByteArrayAsync(requestUrl);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public async Task<GetPersonsResponse> GetAllPersonsInPersonModel(string personModelId)
        {
            string accountAccessToken = await this.GetAccountAccessTokenString(false);
            string requestUrl = $"https://api.videoindexer.ai" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Customization/PersonModels" +
                $"/{personModelId}/Persons" +
                $"?accessToken={accountAccessToken}";
            var result = await this.HttpClient.GetFromJsonAsync<GetPersonsResponse>(requestUrl);
            return result;
        }

        public async Task<string> GetAccountAccessTokenString(bool allowEdit)
        {
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
                $"Auth/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/AccessToken" +
                $"?allowEdit={allowEdit}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                this.AzureConfiguration.VideoIndexerConfiguration.SubscriptionKey);
            var result = await client.GetStringAsync(requestUrl);
            return result.Replace("\"", "");
        }

        public async Task<PersonModel[]> GetAllPersonsModels()
        {
            var accountAccessToken = await this.GetAccountAccessTokenString(false);
            string requestUrl = $"https://api.videoindexer.ai" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}/Accounts/" +
                $"{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}/Customization" +
                $"/PersonModels" +
                $"?accessToken={accountAccessToken}";
            var result = await this.HttpClient.GetFromJsonAsync<PersonModel[]>(requestUrl);
            return result;
        }
    }
}
