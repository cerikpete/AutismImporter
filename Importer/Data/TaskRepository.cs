using System.Linq;
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

        public void GetTasksForStudentForCurrentDay(int studentId)
        {
            var tasksForStudent = (from task in db.Table<Task>()
                                   where task.ParentId == 0
                                   orderby task.SortOrder
                                   select task);
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