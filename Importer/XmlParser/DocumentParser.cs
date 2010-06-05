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
            var tasksForDay = xmlDocument.Descendants("child").Where(x => x.Element("name").Value.StartsWith(nameOfDay) && x.Element("task-type").Value == "day");
            foreach (var taskForDay in tasksForDay.Descendants("child"))
            {
                yield return new Task {name = taskForDay.Element("name").Value};
            }
        }

        public IEnumerable<Task> GetTasksForCurrentDay()
        {
            return GetTasksForCurrentDay(DateTime.Now.DayOfWeek.ToString());
        }
    }
}