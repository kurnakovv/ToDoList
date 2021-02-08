using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Core;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Abstract;

namespace ToDoList.Test.Data.Repositories
{
    [TestClass]
    public class TaskCategoryRepositoryTest
    {
        [TestMethod]
        public void AddCategory_CanAddValidCategory_ReturnCategory()
        {
            // Arrange
            var category = new TaskCategoryEntity()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Daily",
            };

            ITaskCategoryRepository repository = new TaskCategoryRepository();
            
            // Act
            var result = repository.AddCategory(category);
            repository.DeleteCategoryById(category.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        public void UpdateCategory_CanUpdateValidCategory_ReturnCategory()
        {
            // Arrange
            var category = new TaskCategoryEntity()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Daily",
            };

            ITaskCategoryRepository repository = new TaskCategoryRepository();
            repository.AddCategory(category);

            // Act
            var result = repository.UpdateCategory(category);
            repository.DeleteCategoryById(category.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void UpdateCategory_CannotUpdateIfItDoesntExist_ReturnObjectNotFoundException()
        {
            // Arrange
            var category = new TaskCategoryEntity()
            {
                Id = "Guid 1",
                DateTime = DateTime.Now,
                Title = "Daily",
            };

            ITaskCategoryRepository repository = new TaskCategoryRepository();

            // Act
            repository.UpdateCategory(category);
        }
    }
}
