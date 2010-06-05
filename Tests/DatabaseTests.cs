using System;
using System.IO;
using System.Linq;
using Importer.DTOs;
using NUnit.Framework;
using SQLite;

namespace Tests
{
    public class BaseDatabaseTests
    {
        protected SQLiteConnection db;

        [SetUp]
        public void SetUp()
        {
            db = new DatabaseFactory().InitDb();
        }
    }

    [TestFixture]
    public class WhenSavingATaskToTheDatabase : BaseDatabaseTests
    {
        [SetUp]
        public void SetUpData()
        {
            var task = new Task();
            task.Name = "name";
            db.Insert(task);
        }

        [Test]
        public void ShouldBeAbleToRetrieveARowFromTheDatabase()
        {
            var tasks = (from t in db.Table<Task>() select t);
            Assert.AreEqual(1, tasks.Count());
        }
    }
}