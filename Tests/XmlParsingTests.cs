using System.Linq;
using System.Xml.Linq;
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
            var tasks = documentParser.XmlDocument.Descendants("child");
            var monday = tasks.Where(x => x.Element("name").Value == "Monday 1" && x.Element("task-type").Value == "day");
            Assert.AreEqual(1, monday.Count());
        }
    }
}