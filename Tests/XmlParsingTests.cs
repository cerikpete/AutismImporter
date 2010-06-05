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
            Assert.AreEqual(1, tasks.Count());
            Assert.AreEqual("Brush Teeth", tasks.First().Name);
            Assert.AreEqual("Open Toothpaste", tasks.First().Children.First().Name);
        }
    }
}