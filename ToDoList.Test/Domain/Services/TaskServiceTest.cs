using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;

namespace ToDoList.Test.Domain.Services
{
    [TestClass]
    public class TaskServiceTest
    {
        [TestMethod]
        public void CanAddValidTask_ReturnTask()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskService taskService = new TaskService();

            // Act
            var result = taskService.AddTask(taskModel);
            taskService.DeleteTaskById(taskModel.Id);

            // Assert
            Assert.IsNotNull(taskModel);
            Assert.IsNotNull(taskService);

            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CanotAddInvalidTask_ReturnException()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = null,
                Completeness = false,
                DateTime = DateTime.Now,
                Description = " ",
                Name = "",
            };
            ITaskService taskService = new TaskService();

            // Act
            var result = taskService.AddTask(taskModel);

            // Assert
            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        public void CanGetTaskByValidId_ReturnTask()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskService taskService = new TaskService();

            // Act
            taskService.AddTask(taskModel);
            var result = taskService.GetTaskById(taskModel.Id);
            taskService.DeleteTaskById(taskModel.Id);
            // Assert
            Assert.IsNotNull(taskModel);
            Assert.IsNotNull(result);

            Assert.AreEqual(taskModel.Id, result.Id);
            Assert.AreEqual(taskModel.Completeness, result.Completeness);
            Assert.AreEqual(taskModel.DateTime, result.DateTime);
            Assert.AreEqual(taskModel.Description, result.Description);
            Assert.AreEqual(taskModel.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotGetTaskByInvalidId_ReturnException()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = null,
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskService taskService = new TaskService();

            // Act
            var result = taskService.GetTaskById(taskModel.Id);

            // Assert
            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        public void CanUpdateValidTask_ReturnTask()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskService taskService = new TaskService();

            // Act
            var result = taskService.UpdateTask(taskModel);

            // Assert
            Assert.IsNotNull(taskModel);
            Assert.IsNotNull(taskService);

            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotUpdateInvalidTask_ReturnException()
        {
            // Arrange
            TaskModel taskModel = new TaskModel()
            {
                Id = " ",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = " ",
                Name = null,
            };
            ITaskService taskService = new TaskService();

            // Act
            var result = taskService.UpdateTask(taskModel);

            // Assert
            Assert.AreEqual(taskModel, result);
        }
    }
}
