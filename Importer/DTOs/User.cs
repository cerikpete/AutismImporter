using System.Collections.Generic;

namespace Importer.DTOs
{
    public class User
    {
        public string name { get; set; }
        public List<Task> tasks { get; set; }
    }
}