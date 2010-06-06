using System.Collections.Generic;
using SQLite;

namespace Importer.DTOs
{
    public class Task
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [Ignore]
        public IEnumerable<Task> Children { get; set; }
        public bool Completed { get; set; }
    }
}