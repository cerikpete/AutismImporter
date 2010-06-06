using System;   
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Importer.DTOs;

namespace Importer.XmlParser
{
    public class DocumentParser
    {
        private readonly XDocument xmlDocument;
        private int studentId;

        public DocumentParser()
        {
            xmlDocument = XDocument.Load(@"C:\Users\Erik\Desktop\full.xml");
        }

        public XDocument XmlDocument
        {
            get { return xmlDocument; }
        }

        public IEnumerable<Task> GetTasksForCurrentDay(string nameOfDay)
        {
            // Get the ID of the current student
            var userNode = xmlDocument.Descendants("user");
            studentId = int.Parse(userNode.First().Element("id").Value);

            // Get the node from the xml document representing the current day
            var nodeForDay = xmlDocument.Descendants("child").Where(x => x.Element("name").Value.StartsWith(nameOfDay) && x.Element("task-type").Value == "day").First();

            // Get the children element of this node
            var tasksForDay = nodeForDay.Elements("children");
            if (tasksForDay != null && tasksForDay.Count() > 0)
            {
                return LoadTasksForItem(tasksForDay, 0);
            }
            return new List<Task>();
        }

        private IEnumerable<Task> LoadTasksForItem(IEnumerable<XElement> childrenElement, int parentId)
        {
            // Get the "child" elements of this element, which would be the tasks
            var tasks = childrenElement.Elements("child");
            if (tasks != null && tasks.Count() > 0)
            {
                foreach (var task in tasks)
                {
                    yield return CreateTask(task, parentId);
                }
            }
        }

        private Task CreateTask(XElement element, int parentId)
        {
            var task = new Task { Id = int.Parse(element.Element("id").Value), 
                                    Name = element.Element("name").Value, 
                                    ParentId = parentId,
                                    SortOrder = int.Parse(element.Element("sort-order").Value),
                                    StudentId = studentId};
            var children = element.Elements("children");
            var hasChildren = (children != null && children.Count() > 0);
            if (hasChildren)
            {
                var taskId = int.Parse(element.Element("id").Value);
                task.Children = LoadTasksForItem(children, taskId);
            }
            return task;
        }

        public IEnumerable<Task> GetTasksForCurrentDay()
        {
            return GetTasksForCurrentDay(DateTime.Now.DayOfWeek.ToString());
        }
    }
}