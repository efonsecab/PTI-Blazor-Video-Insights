using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PTIBlazorVideoInsightsCourse.Shared.Models;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersons;

namespace PTIBlazorVideoInsightsCourse.Client.Pages
{
    public partial class Persons
    {
        [Parameter]
        public string PersonModelId { get; set; }
        private bool IsLoading { get; set; }
        private GetPersonsResponse PersonsResult { get; set; }
        private GetAllPersonsModel GetAllPersonsResult { get; set; }
        [Inject]
        private HttpClient httpClient { get; set; }
        private Dictionary<string, string> PersonsPictures { get; set; } = new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.IsLoading = true;
                if (this.PersonModelId != null)
                {
                    var tmpPersonsResult = await httpClient.GetFromJsonAsync<GetPersonsResponse>
                        ($"VideoIndexer/GetPersons?personModelId={this.PersonModelId}");
                    foreach (var singlePerson in tmpPersonsResult.results)
                    {
                        string base64Image = await httpClient.GetStringAsync($"VideoIndexer" +
                            $"/GetPersonPicture?personModelId={this.PersonModelId}" +
                            $"&personId={singlePerson.id}" +
                            $"&faceId={singlePerson.sampleFace.id}");
                        string base64ImgSrcString = String.Format("data:image/gif;base64,{0}", base64Image);
                        this.PersonsPictures.Add(singlePerson.id, base64ImgSrcString);
                    }
                    this.PersonsResult = tmpPersonsResult;
                }
                else
                {
                    this.GetAllPersonsResult =
                        await httpClient.GetFromJsonAsync<GetAllPersonsModel>("VideoIndexer/GetAllPersons");
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
