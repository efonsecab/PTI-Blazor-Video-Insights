using System;
using System.Collections.Generic;

namespace PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersonModels
{
    public class PersonModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isDefault { get; set; }
        public int personsCount { get; set; }
    }

    public class GetPersonModelsResponse
    {
        public List<PersonModel> PersonModel { get; set; }
    }
}
