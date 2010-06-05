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
            jsonText = "{\"user\": {\"name\": \"Bob\"}}";
            serializer = new JavaScriptSerializer();
        }

        [Test]
        public void ShouldCorrectlySerializeJsonToObjects()
        {
            var userContainer = serializer.Deserialize<UserContainer>(jsonText);
            Assert.AreEqual("Bob", userContainer.user.name);
        }
    }
}