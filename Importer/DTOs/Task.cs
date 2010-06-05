using System.Collections.Generic;

namespace Importer.DTOs
{
    public class Task
    {
        public string Name { get; set; }
        public IEnumerable<Task> Children { get; set; }
    }
}