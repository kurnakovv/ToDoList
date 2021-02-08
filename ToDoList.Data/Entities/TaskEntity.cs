using System.Collections.Generic;
using ToDoList.Data.Entities.Abstract;

namespace ToDoList.Data.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completeness { get; set; }
        public TaskCategoryEntity Category { get; set; }
    }
}
