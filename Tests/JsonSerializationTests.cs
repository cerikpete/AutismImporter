using System.Web.Script.Serialization;
using Importer.DTOs;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JsonSerializationTests
    {
        private string jsonText;
        private JavaScriptSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            jsonText = "{\"data\": {\"user\": {\"name\": \"Bob\"}}}";
            serializer = new JavaScriptSerializer();
        }

        [Test]
        public void ShouldCorrectlySerializeJsonToObjects()
        {
            var dataContainer = serializer.Deserialize<DataContainer>(jsonText);
            Assert.AreEqual("Bob", dataContainer.data.user.name);
        }
    }
}