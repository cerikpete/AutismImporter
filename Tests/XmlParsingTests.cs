using System.Collections.Generic;
using System.Linq;
using Importer.DTOs;
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
    public class WhenParsingTheXmlDocumentAndTheDayHasOneTaskWithOneStep : BaseXmlParsingTests
    {
        private IEnumerable<Task> tasks;

        [SetUp]
        public void SetUpDaysTasks()
        {
            tasks = documentParser.GetTasksForCurrentDay("Tuesday");          
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheTasksCorrectly()
        {
            Assert.AreEqual(1, tasks.Count());
            Assert.AreEqual("Brush Teeth", tasks.First().Name);
            Assert.AreEqual(0, tasks.First().ParentId);
            Assert.AreEqual(19, tasks.First().Id);
        }

        [Test]
        public void ShouldBeAbleToRetrieveTheTasksStepsCorrectly()
        {
            Assert.AreEqual("Open Toothpaste", tasks.First().Children.First().Name);            
            Assert.AreEqual(19, tasks.First().Children.First().ParentId);
        }
    }
}