using System;
using System.Collections.Generic;
using System.Text;

namespace PTIBlazorVideoInsightsCourse.AutomatedTests.Server.Configuration
{
    public class AzureConfiguration
    {
        public VideoIndexerConfiguration VideoIndexerConfiguration { get; set; }
    }

    public class VideoIndexerConfiguration
    {
        public string BaseAPIUrl { get; set; }
        public string SubscriptionKey { get; set; }
    }
}
