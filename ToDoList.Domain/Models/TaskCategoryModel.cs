using System;
using System.Collections.Generic;

namespace ToDoList.Domain.Models
{
    public class TaskCategoryModel
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public ICollection<TaskModel> Tasks { get; set; }

        public TaskCategoryModel()
        {
            Id = Guid.NewGuid().ToString();
            DateTime = DateTime.Now;
            Tasks = new List<TaskModel>();
        }

        public static TaskCategoryModel GetCloneCategory(TaskCategoryModel category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return new TaskCategoryModel()
            {
                Title = category.Title,
                DateTime = category.DateTime,
                Tasks = category.Tasks,
                Id = category.Id,
            };
        }
    }
}
