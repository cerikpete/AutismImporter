using System.Collections.Generic;

namespace Importer.DTOs
{
    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sort_order { get; set; }
        public string task_type { get; set; }
        public IEnumerable<Task> children { get; set; }
    }
}