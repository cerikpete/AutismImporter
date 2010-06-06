using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Importer.DTOs;
using Importer.XmlParser;
using SQLite;

namespace Importer.Data
{
    public class TaskRepository
    {
        private readonly DocumentParser documentParser;
        private SQLiteConnection db;

        public TaskRepository(DocumentParser documentParser)
        {
            this.documentParser = documentParser;
            db = new DatabaseFactory().InitDb();
        }

        public void ImportTasksForTheCurrentDay() //TODO: Will need the student id
        {
            var tasksToImport = documentParser.GetTasksForCurrentDay();
            foreach (var task in tasksToImport)
            {
                SaveTask(task);
            }
        }

        public void ImportTasksForTheCurrentDay(string dayName) //TODO: Remove
        {
            var tasksToImport = documentParser.GetTasksForCurrentDay(dayName);
            foreach (var task in tasksToImport)
            {
                SaveTask(task);
            }
        }

        public IEnumerable<Task> GetTasksForStudentForCurrentDay(int studentId)
        {
            var tasksForStudent = (from task in db.Table<Task>()
                                   where task.ParentId == 0
                                   orderby task.SortOrder
                                   select task);
            return tasksForStudent;
        }

        public void ImportMedia(string fileUri)
        {
            var fileName = Path.GetFileName(fileUri);
            var savePathForFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);            
            if (!File.Exists(savePathForFile))
            {
                var client = new WebClient();
                BinaryWriter binaryWriter = null;
                try
                {
                    byte[] data = client.DownloadData(fileUri);
                    var fileStream = new FileStream(savePathForFile, FileMode.Create, FileAccess.Write);
                    binaryWriter = new BinaryWriter(fileStream);
                    binaryWriter.Write(data);
                    binaryWriter.Close();
                }
                finally
                {
                    client.Dispose();
                    if (binaryWriter != null)
                    {
                        binaryWriter.Close();
                    }
                }
            }
        }

        private void SaveTask(Task task)
        {
            db.Insert(task);
            if (task.Children != null && task.Children.Count() > 0)
            {
                foreach (var childTask in task.Children)
                {
                    SaveTask(childTask);
                }
            }
        }
    }
}