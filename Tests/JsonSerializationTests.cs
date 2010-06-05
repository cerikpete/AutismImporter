using System.Linq;
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
            jsonText = "{\"data\": {\"user\": {\"name\": \"Bob\", \"tasks\": [{\"name\": \"Task 1\", \"id\": 1, \"task_type\": \"day\", \"sort_order\": 2}, {\"name\": \"Task 2\", \"children\": [{\"name\": \"child 1\"}]}]}}}";
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
            var firstTask = dataContainer.data.user.tasks[0];
            var secondTask = dataContainer.data.user.tasks[1];
            Assert.AreEqual("Task 1", firstTask.name);
            Assert.AreEqual("Task 2", secondTask.name);
            Assert.AreEqual(1, firstTask.id);
            Assert.AreEqual(2, firstTask.sort_order);
            Assert.AreEqual(1, secondTask.children.Count());
            Assert.AreEqual("child 1", secondTask.children.First().name);
            Assert.AreEqual("day", firstTask.task_type);
        }
    }
}