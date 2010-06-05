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
            // Get the node from the xml document representing the current day
            var nodeForDay = xmlDocument.Descendants("child").Where(x => x.Element("name").Value.StartsWith(nameOfDay) && x.Element("task-type").Value == "day").First();

            // Get the children element of this node
            var taksForDay = nodeForDay.Elements("children");
            if (taksForDay != null && taksForDay.Count() > 0)
            {
                return LoadTasksForItem(taksForDay);
            }
            return null;
        }

        private IEnumerable<Task> LoadTasksForItem(IEnumerable<XElement> childrenElement)
        {
            // Get the "child" elements of this element, which would be the tasks
            var tasks = childrenElement.Elements("child");
            if (tasks != null && tasks.Count() > 0)
            {
                foreach (var task in tasks)
                {
                    yield return CreateTask(task);
                }
            }
        }

        /// <summary>
        /// for the element, 
        ///     load task
        ///     does it have a children node with one or more elements?
        ///         if so, load task for each child element
        /// </summary>

        private Task CreateTask(XElement element)
        {
            var task = new Task { name = element.Element("name").Value };
            var children = element.Elements("children");
            var hasChildren = (children != null && children.Count() > 0);
            if (hasChildren)
            {
                task.children = LoadTasksForItem(children);
            }
            return task;
        }

        public IEnumerable<Task> GetTasksForCurrentDay()
        {
            return GetTasksForCurrentDay(DateTime.Now.DayOfWeek.ToString());
        }
    }
}