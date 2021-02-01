using System.Collections.Generic;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Repositories.Abstract
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> GetAllTasks();
        TaskEntity AddTask(TaskEntity task);
        TaskEntity UpdateTask(TaskEntity task);
        TaskEntity GetTaskById(string id);
        IEnumerable<TaskEntity> GetTasksByName(string name);
        void DeleteTaskById(string id);
    }
}
