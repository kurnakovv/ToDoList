using System;

namespace ToDoList.Domain.Models
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completeness { get; set; }
        public DateTime DateTime { get; set; }
        public string CategoryId { get; set; }
        public TaskCategoryModel Category { get; set; }

        public TaskModel()
        {
            Id = Guid.NewGuid().ToString();
            DateTime = DateTime.Now;
        }

        public static TaskModel GetCloneTask(TaskModel task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return new TaskModel()
            {
                Name = task.Name,
                Description = task.Description,
                Completeness = task.Completeness,
                DateTime = task.DateTime,
                CategoryId = task.CategoryId,
                Category = task.Category,
            };
        }
    }
}
