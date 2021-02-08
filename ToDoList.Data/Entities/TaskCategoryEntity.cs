using System.Collections.Generic;
using ToDoList.Data.Entities.Abstract;

namespace ToDoList.Data.Entities
{
    public class TaskCategoryEntity : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<TaskEntity> TasksCategory { get; set; }

        public TaskCategoryEntity()
        {
            TasksCategory = new List<TaskEntity>();
        }
    }
}
