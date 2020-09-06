using System;
using System.Collections.Generic;

namespace PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetPersons
{
    public class SampleFace
    {
        public string id { get; set; }
        public string state { get; set; }
        public string sourceType { get; set; }
        public string sourceVideoId { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public string name { get; set; }
        public SampleFace sampleFace { get; set; }
        public DateTime lastModified { get; set; }
        public string lastModifierName { get; set; }
    }

    public class GetPersonsResponse
    {
        public List<Person> results { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
    }


}
