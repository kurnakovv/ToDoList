using System.Collections.Generic;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.Services.Abstract
{
    public interface ITaskService
    {
        IEnumerable<TaskModel> GetAllTasks();
        TaskModel AddTask(TaskModel task);
        TaskModel UpdateTask(TaskModel task);
        TaskModel GetTaskById(string id);
        IEnumerable<TaskModel> GetTasksByName(string name);
        void DeleteTaskById(string id);
        IEnumerable<TaskModel> SortTasksByCategory(string categoryName);
    }
}
