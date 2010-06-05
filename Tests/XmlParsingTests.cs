using System.Linq;
using Importer.XmlParser;
using NUnit.Framework;

namespace Tests
{
    public class BaseXmlParsingTests
    {
        protected DocumentParser documentParser;

        [SetUp]
        public void SetUp()
        {
            documentParser = new DocumentParser();
        }
    }

    [TestFixture]
    public class WhenParsingTheXmlDocument : BaseXmlParsingTests
    {
        [Test]
        public void ShouldBeAbleToRetrieveTasksOfTypeDay()
        {
            var mondayTasks = documentParser.GetTasksForCurrentDay();
            Assert.AreEqual(1, mondayTasks.Count());
        }
    }
}