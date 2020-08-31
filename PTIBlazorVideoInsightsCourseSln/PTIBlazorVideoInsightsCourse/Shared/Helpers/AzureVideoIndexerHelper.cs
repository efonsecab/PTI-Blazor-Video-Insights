using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PTIBlazorVideoInsightsCourse.Shared.Models;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersonModels;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersons;

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
