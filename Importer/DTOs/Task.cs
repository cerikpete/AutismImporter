using System.Collections.Generic;

namespace Importer.DTOs
{
    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sort_order { get; set; }
        public List<Task> children { get; set; }
    }
}