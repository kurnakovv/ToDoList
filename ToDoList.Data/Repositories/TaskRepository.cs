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

        public TaskEntity UpdateTask(TaskEntity task)
        {
            var returnedTask = _taskDbContext.Tasks
                                             .SingleOrDefault(dbTask => dbTask.Id == task.Id);
            if (returnedTask is null)
            {
                throw new ArgumentOutOfRangeException("The input task have a incorrect Id!");
            }

            returnedTask.Name = task.Name;
            returnedTask.Completeness = task.Completeness;
            returnedTask.DateTime = task.DateTime;
            returnedTask.Description = task.Description;

            Save();
            return task;
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

        private void Save()
        {
            _taskDbContext.SaveChanges();
        }
    }
}
