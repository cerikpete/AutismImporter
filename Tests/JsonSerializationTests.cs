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
            jsonText = "{\"name\": \"Bob\"}";
            serializer = new JavaScriptSerializer();
        }

        [Test]
        public void ShouldCorrectlySerializeJsonToObjects()
        {
            var o = serializer.Deserialize<User>(jsonText);
            Assert.AreEqual("Bob", o.name);
        }
    }
}