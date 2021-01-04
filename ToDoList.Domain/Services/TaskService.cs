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
            }

            throw new ArgumentException();
        }

        public TaskModel GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskEntity, TaskModel>());
                var mapper = new Mapper(config);

                TaskModel addTask = mapper.Map<TaskModel>(id);
                _taskRepository.GetTaskById(id);
                return addTask;
            }
            throw new ArgumentException();
        }

        public TaskModel UpdateTask(TaskModel task)
        {
            if (!IsTaskNull(task))
            {
                if (!IsTaskPropertiesNull(task))
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskModel, TaskEntity>()
                       .ForMember(t => t.Id, opt => opt.Ignore())

                    );
                    var mapper = new Mapper(config);

                    var updateTask = mapper.Map<TaskModel, TaskEntity>(task);
                    _taskRepository.UpdateTask(updateTask);
                    return task;
                }
            }

            throw new ArgumentException();
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
            if (string.IsNullOrEmpty(task.Name) ||
                string.IsNullOrEmpty(task.Description) ||
                task.DateTime == null)
            {
                return true;
            }

            return false;
        }
    }
}
