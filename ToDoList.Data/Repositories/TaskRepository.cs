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
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    var addTask = _taskDbContext.Tasks.Add(task);
                    Save();

                    return addTask;
                }
            }

            throw new ApplicationException();
        }

        public TaskEntity GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var task = FindTaskById(id);
                return task;
            }

            throw new ApplicationException();
        }

        public TaskEntity UpdateTask(TaskEntity task)
        {
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    _taskDbContext.Entry(task).State = EntityState.Modified;
                    Save();
                    return task;
                }
            }

            throw new ApplicationException();
        }

        public void DeleteTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var removeTask = FindTaskById(id);

                if (IsTaskNull(removeTask))
                    throw new Exception("Empty task cannot be deleted!");

                _taskDbContext.Tasks.Remove(removeTask);
                Save();
            }
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
            if (string.IsNullOrEmpty(taskEntity.Id) ||
               string.IsNullOrEmpty(taskEntity.Name) ||
               string.IsNullOrEmpty(taskEntity.Description) ||
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
