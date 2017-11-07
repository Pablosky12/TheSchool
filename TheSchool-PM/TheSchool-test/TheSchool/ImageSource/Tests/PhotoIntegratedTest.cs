using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSource.Helper;
using ImageSource.Concrete;

namespace ImageSource
{
    [TestClass]
    public class PhotoIntegratedTest
    {
        ISerializer serializer;
        IRestClient client;
        PhotoHelper helper;

        [TestInitialize]
        public void TestInitializer()
        {
            serializer = new SerializerImp();
            client = new RestClientImpl();

            helper = new PhotoHelper(client, serializer);
        }

        [TestMethod]
        public void GetAll_AllFieldsWithValuesTest()
        {
            var output = helper.GetAll();
            helper.Match(x => x.Title.Contains("demo") || x.Url.Contains("demo"));

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0);
            Assert.IsTrue(output.First().PhotoBook > 0);
        }

        [TestMethod]
        public void GetAll_FindMatches()
        {
            var output = helper.Match(x => x.Title.Contains("vol") || x.Url.Contains("vol"));

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0 && output.Count() < 5000);
            Assert.IsTrue(output.First().Title.Contains("vol") || output.First().Url.Contains("vol"));
        }

        [TestMethod]
        public void GetAll_FindMatchesAndSort()
        {
            var output1 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") || x.Url.Contains("dolor"), 
                sorting: x => x.Title);

            var output2 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") || x.Url.Contains("dolor"),
                sorting: x => x.Id);

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.IsTrue(output1.First().Title != output2.First().Title);
            Assert.IsTrue(client.HitCounter > 1);
        }
    }
}
