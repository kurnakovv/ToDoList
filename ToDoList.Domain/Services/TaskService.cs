using AutoMapper;
using System;
using System.Collections.Generic;
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

        //public TaskService(ITaskRepository taskRepository)
        //{
        //    _taskRepository = taskRepository;
        //}

        public IEnumerable<TaskModel> GetAllTasks()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<TaskEntity, TaskModel>());
            var mapper = new Mapper(config);

            var tasks = mapper.Map<IEnumerable<TaskModel>>(_taskRepository.GetAllTasks());

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
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskModel, TaskEntity>());
                    var mapper = new Mapper(config);

                    var addTask = mapper.Map<TaskEntity>(task);
                    _taskRepository.AddTask(addTask);
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
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskEntity, TaskModel>());
                var mapper = new Mapper(config);

                var getTask = _taskRepository.GetTaskById(id);

                if (getTask is null)
                    throw new System.Data.Entity.Core.ObjectNotFoundException("Task not found");

                TaskModel task = mapper.Map<TaskModel>(getTask);

                return task;
            }

            throw new Exception("Task id cannot be empty");
        }

        public TaskModel UpdateTask(TaskModel task)
        {
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskModel, TaskEntity>()
                                    .ForMember(t => t.Id, opt => opt.Ignore()));

                    var mapper = new Mapper(config);

                    var updateTask = mapper.Map<TaskModel, TaskEntity>(task);

                    // Mapper create for updateTask a new Id, this should not be the case, 
                    // for updateTask the previous Id from input TaskModel.
                    updateTask.Id = task.Id;

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
