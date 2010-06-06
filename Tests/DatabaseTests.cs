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
            //var pathToDb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "sessions.db");
            //var conn = new SQLiteConnection(pathToDb);

            //if (!File.Exists(pathToDb))
            //{
            //    File.Copy("sessions.db", pathToDb);
            //}

            db = new DatabaseFactory().InitDb();

            // Empty test Tasks
            var tasks = (from t in db.Table<Task>() select t);
            foreach (var task in tasks)
            {
                db.Delete(task);
            }            
        }

        [TearDown]
        public void TearDown()
        {
            db.Dispose();
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
            Assert.AreEqual("name", tasks.First().Name);
        }
    }
}