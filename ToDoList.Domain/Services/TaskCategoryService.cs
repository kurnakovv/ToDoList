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
    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository =
            new TaskCategoryRepository();

        public TaskCategoryModel AddCategory(TaskCategoryModel category)
        {
            if (IsCategoryNull(category))
                throw new Exception("Cannot be add empty category.");

            if (IsCategoryPropertiesNull(category))
                throw new Exception("Cannot be add category with empty properties.");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskCategoryModel, TaskCategoryEntity>());
            var mapper = new Mapper(config);
            var categoryEntity = mapper.Map<TaskCategoryEntity>(category);

            _taskCategoryRepository.AddCategory(categoryEntity);

            return category;
        }

        public void DeleteCategoryById(string id)
        {
            if (id is null)
                throw new Exception("Id cannot be empty");

            _taskCategoryRepository.DeleteCategoryById(id);
        }

        public IEnumerable<TaskCategoryModel> GetCategories()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskCategoryEntity, TaskCategoryModel>());
            var mapper = new Mapper(config);
            var categories = mapper.Map<IEnumerable<TaskCategoryModel>>(_taskCategoryRepository.GetCategories());

            if (categories is null)
                throw new Exception("Database not connected.");

            return categories;
        }

        public TaskCategoryModel UpdateCategory(TaskCategoryModel category)
        {
            if (IsCategoryNull(category))
                throw new Exception("Cannot be update empty category.");

            if (IsCategoryPropertiesNull(category))
                throw new Exception("Cannot be update category with empty properties.");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskCategoryModel, TaskCategoryEntity>());
            var mapper = new Mapper(config);
            var updateCategory = mapper.Map<TaskCategoryModel, TaskCategoryEntity>(category);
            updateCategory.Id = category.Id;

            _taskCategoryRepository.UpdateCategory(updateCategory);
            return category;
        }

        private bool IsCategoryNull(TaskCategoryModel category)
        {
            if (category is null)
                return true;

            return false;
        }

        private bool IsCategoryPropertiesNull(TaskCategoryModel category)
        {
            if (string.IsNullOrWhiteSpace(category.Title))
            {
                return true;
            }

            return false;
        }
    }
}
