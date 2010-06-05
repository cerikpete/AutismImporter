using System.Collections.Generic;

namespace Importer.DTOs
{
    public class User
    {
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }
}