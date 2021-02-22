using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Core;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;
using ToDoList.Mapping.Automapper;

namespace ToDoList.Test.Domain.Services
{
    [TestClass]
    public class TaskCategoryServiceTest
    {
        [TestMethod]
        public void AddCategory_CanAddValidCategory_ReturnCategory()
        {
            // Arrange
            var category = new TaskCategoryModel()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Daily",
            };

            ITaskCategoryService repository = new TaskCategoryService(AutomapperConfig.MapConfig());

            // Act
            var result = repository.AddCategory(category);
            repository.DeleteCategoryById(category.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddCategory_CannotAddInvalidCategory_ReturnException()
        {
            // Arrange
            var category = new TaskCategoryModel()
            {
                Id = " ",
                Title = null,
            };

            ITaskCategoryService repository = new TaskCategoryService(AutomapperConfig.MapConfig());

            // Act
            repository.AddCategory(category);
        }

        [TestMethod]
        public void UpdateCategory_CanUpdateValidCategory_ReturnCategory()
        {
            // Arrange
            var category = new TaskCategoryModel()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Daily",
            };

            var updateCategory = new TaskCategoryModel()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Title",
            };

            ITaskCategoryService repository = new TaskCategoryService(AutomapperConfig.MapConfig());

            // Act
            repository.AddCategory(category);
            var result = repository.UpdateCategory(updateCategory);
            repository.DeleteCategoryById(category.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateCategory, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateCategory_CannotUpdateInvalidCategory_ReturnException()
        {
            // Arrange
            var category = new TaskCategoryModel()
            {
                Id = null,
                DateTime = DateTime.Now,
                Title = "     ",
            };

            ITaskCategoryService repository = new TaskCategoryService(AutomapperConfig.MapConfig());

            // Act
            repository.UpdateCategory(category);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteCategoryById_CannotDeleteByInvalidId_ReturnException()
        {
            // Arrange
            string id = null;

            ITaskCategoryService repository = new TaskCategoryService(AutomapperConfig.MapConfig());

            // Act
            repository.DeleteCategoryById(id);
        }
    }
}
