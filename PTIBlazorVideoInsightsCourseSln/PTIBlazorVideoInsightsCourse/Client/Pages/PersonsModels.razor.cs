using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersonModels;

namespace PTIBlazorVideoInsightsCourse.Client.Pages
{
    public partial class PersonsModels
    {
        private bool IsLoading { get;  set; }
        private PersonModel[] PersonsModelsResult { get; set; }
        [Inject]
        private HttpClient httpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.IsLoading = true;
                this.PersonsModelsResult = await httpClient.GetFromJsonAsync<PersonModel[]>
                    ("VideoIndexer/GetPersonsModels");
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
