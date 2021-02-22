using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.Data.Context;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Abstract;

namespace ToDoList.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _taskDbContext = new TaskDbContext();

        //public TaskRepository(TaskDbContext taskDbContext)
        //{
        //    _taskDbContext = taskDbContext;
        //}

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            IEnumerable<TaskEntity> tasksList = _taskDbContext.Tasks.ToList();
            return tasksList;
        }

        public TaskEntity AddTask(TaskEntity task)
        {
            var addTask = _taskDbContext.Tasks.Add(task);
            Save();

            return addTask;
        }

        public TaskEntity GetTaskById(string id)
        {
            var task = FindTaskById(id);
            return task;
        }

        public IEnumerable<TaskEntity> GetTasksByName(string name)
        {
            IEnumerable<TaskEntity> tasks = FindTasksByName(name);
            return tasks;
        }

        public TaskEntity UpdateTask(TaskEntity task)
        {
            var returnedTask = FindTaskById(task.Id);
            if (IsTaskNull(returnedTask))
            {
                throw new System.Data.Entity.Core.
                    ObjectNotFoundException("The task not found.");
            }

            _taskDbContext.Entry(returnedTask).CurrentValues.SetValues(task);

            Save();
            return returnedTask;
        }

        public IEnumerable<TaskEntity> SortTasksByCategory(string categoryName)
        {
            IEnumerable<TaskEntity> tasks = _taskDbContext.Tasks.Where(t => 
                                                t.Category.Title.Contains(categoryName));

            return tasks;
        }

        public void DeleteTaskById(string id)
        {
            var removeTask = FindTaskById(id);

            if (IsTaskNull(removeTask))
                throw new Exception("Empty task cannot be deleted!");

            _taskDbContext.Tasks.Remove(removeTask);
            Save();
        }

        private bool IsTaskNull(TaskEntity task)
        {
            if (task is null)
            {
                return true;
            }

            return false;
        }

        private bool IsTaskPropertiesNull(TaskEntity taskEntity)
        {
            if (string.IsNullOrWhiteSpace(taskEntity.Id) ||
               string.IsNullOrWhiteSpace(taskEntity.Name) ||
               string.IsNullOrWhiteSpace(taskEntity.Description) ||
               taskEntity.DateTime == null)
            {
                return true;
            }

            return false;
        }

        private TaskEntity FindTaskById(string id)
        {
            return _taskDbContext.Tasks.Find(id);
        }

        private IEnumerable<TaskEntity> FindTasksByName(string name)
        {
            return _taskDbContext.Tasks.Where(t => t.Name.Contains(name));
        }

        private void Save()
        {
            _taskDbContext.SaveChanges();
        }
    }
}
