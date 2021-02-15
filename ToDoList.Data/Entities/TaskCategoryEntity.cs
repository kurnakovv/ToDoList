using System.Collections.Generic;
using ToDoList.Data.Entities.Abstract;

namespace ToDoList.Data.Entities
{
    public class TaskCategoryEntity : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }

        public TaskCategoryEntity()
        {
            Tasks = new List<TaskEntity>();
        }
    }
}
