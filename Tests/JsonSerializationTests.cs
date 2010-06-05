using System.Web.Script.Serialization;
using Importer.DTOs;
using NUnit.Framework;

namespace Tests
{
    public class BaseJsonSerializationTests
    {
        protected string jsonText;
        protected JavaScriptSerializer serializer;
        protected DataContainer dataContainer;

        [SetUp]
        public void SetUp()
        {
            jsonText = "{\"data\": {\"user\": {\"name\": \"Bob\", \"tasks\": [{\"name\": \"Task 1\"}, {\"name\": \"Task 2\"}]}}}";
            serializer = new JavaScriptSerializer();
            dataContainer = serializer.Deserialize<DataContainer>(jsonText);
        }
    }

    [TestFixture]
    public class WhenSerializingTheJsonObject : BaseJsonSerializationTests
    {
        [Test]
        public void ShouldCorrectlySerializeTheUsersName()
        {
            Assert.AreEqual("Bob", dataContainer.data.user.name);
        }

        [Test]
        public void ShouldCorrectlySerializeTheUsersTasks()
        {
            Assert.AreEqual(2, dataContainer.data.user.tasks.Count);
            Assert.AreEqual("Task 1", dataContainer.data.user.tasks[0].name);
            Assert.AreEqual("Task 2", dataContainer.data.user.tasks[1].name);
        }
    }
}