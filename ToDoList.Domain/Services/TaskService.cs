using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Abstract;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services.Abstract;

namespace ToDoList.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository = new TaskRepository();
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            var tasksEntities = _taskRepository.GetAllTasks();

            var tasks = _mapper.Map<IEnumerable<TaskModel>>(tasksEntities);

            if (tasks is null)
                throw new Exception();

            return tasks;
        }

        public TaskModel AddTask(TaskModel task)
        {
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    var taskEntity = _mapper.Map<TaskEntity>(task);
                    _taskRepository.AddTask(taskEntity);
                    return task;
                }

                throw new Exception("Cannot be add task with empty fields!");
            }

            throw new Exception("Cannot be add empty task!");
        }

        public TaskModel GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var taskEntity = _taskRepository.GetTaskById(id);
                var taskModel = _mapper.Map<TaskModel>(taskEntity);

                if (taskModel is null)
                    throw new System.Data.Entity.Core.ObjectNotFoundException("Task not found");

                return taskModel;
            }

            throw new Exception("Task id cannot be empty");
        }

        public IEnumerable<TaskModel> GetTasksByName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Task name cannot be empty");
            }

            var tasksEntity = _taskRepository.GetTasksByName(name);
            var tasksModel = _mapper.Map<IEnumerable<TaskModel>>(tasksEntity);

            if (tasksModel.Count() == 0)
                throw new System.Data.Entity.Core.ObjectNotFoundException("Tasks not found");

            return tasksModel;
        }

        public IEnumerable<TaskModel> SortTasksByCategory(string categoryName)
        {
            var tasksEntity = _taskRepository.SortTasksByCategory(categoryName);

            if(tasksEntity.Count() == 0)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException($"The category \"{categoryName}\" is empty");
            }

            var tasksModel = _mapper.Map<IEnumerable<TaskModel>>(tasksEntity);

            return tasksModel;
        }

        public TaskModel UpdateTask(TaskModel task)
        {
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    var updateTask = _mapper.Map<TaskEntity>(task);

                    _taskRepository.UpdateTask(updateTask);
                    return task;
                }

                throw new Exception("Cannot be update task with empty fields!");
            }

            throw new Exception("Cannot be update empty task!");
        }

        public void DeleteTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _taskRepository.DeleteTaskById(id);
            }
        }

        private bool IsTaskNull(TaskModel task)
        {
            if (task is null)
            {
                return true;
            }

            return false;
        }

        private bool IsTaskPropertiesNull(TaskModel task)
        {
            if (string.IsNullOrWhiteSpace(task.Name) ||
                string.IsNullOrWhiteSpace(task.Description) ||
                task.DateTime == null)
            {
                return true;
            }

            return false;
        }
    }
}
