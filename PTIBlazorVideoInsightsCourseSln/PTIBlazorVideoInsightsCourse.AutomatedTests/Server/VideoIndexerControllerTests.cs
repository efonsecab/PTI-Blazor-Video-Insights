using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTIBlazorVideoInsightsCourse.AutomatedTests.Server
{
    [TestClass]
    public class VideoIndexerControllerTests
    {
        private Server.Configuration.VideoIndexerConfiguration VideoIndexerConfiguration { get; set; }
        [TestInitialize]
        public void InitializeTests()
        {
            this.VideoIndexerConfiguration =
                new Configuration.VideoIndexerConfiguration()
                {
                    BaseAPIUrl="",
                    SubscriptionKey = ""
                };
        }

        [TestMethod]
        public void ListVideosTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAccountAccessToken()
        {
            Assert.Inconclusive();
        }
    }
}
