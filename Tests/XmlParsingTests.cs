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
            var tasks = documentParser.GetTasksForCurrentDay("Tuesday");
            Assert.AreEqual(2, tasks.Count());
            Assert.AreEqual("Brush Teeth", tasks.First().name);
        }
    }
}