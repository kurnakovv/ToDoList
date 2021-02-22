using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;
using ToDoList.Mapping.Automapper;

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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

            // Act
            var result = taskService.AddTask(taskModel);
            taskService.DeleteTaskById(taskModel.Id);

            // Assert
            Assert.IsNotNull(taskModel);
            Assert.IsNotNull(taskService);

            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
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

            TaskModel taskModel2 = null;
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

            // Act
            var result1 = taskService.AddTask(taskModel);
            var result2 = taskService.AddTask(taskModel2);

            // Assert
            Assert.AreEqual(taskModel, result1);
            Assert.AreEqual(taskModel, result2);
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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

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
        [ExpectedException(typeof(Exception))]
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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

            // Act
            var result = taskService.GetTaskById(taskModel.Id);

            // Assert
            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Core.ObjectNotFoundException))]
        public void CannotGetTaskByIdIfTaskNotFound_ReturnException()
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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());
            taskService.AddTask(taskModel);

            // Act
            var result = taskService.UpdateTask(taskModel);
            taskService.DeleteTaskById(taskModel.Id);

            // Assert
            Assert.IsNotNull(taskModel);
            Assert.IsNotNull(taskService);

            Assert.AreEqual(taskModel, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
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

            TaskModel taskModel2 = null;

            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());

            // Act
            var result1 = taskService.UpdateTask(taskModel);
            var result2 = taskService.UpdateTask(taskModel2);

            // Assert
            Assert.AreEqual(taskModel, result1);
            Assert.AreEqual(taskModel, result2);
        }

        [TestMethod]
        public void CanGetTasksByValidName()
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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());
            taskService.AddTask(taskModel);

            // Act
            var result = taskService.GetTasksByName(taskModel.Name);
            // Assert
            Assert.AreEqual(taskModel.Name, result.FirstOrDefault(t => t.Name == taskModel.Name).Name);

            taskService.DeleteTaskById(taskModel.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Core.ObjectNotFoundException))]
        public void CannotGetTasksIfCountEqualsNull_ReturnObjectNotFoundException()
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
            ITaskService taskService = new TaskService(AutomapperConfig.MapConfig());
            taskService.GetTasksByName(taskModel.Name);
        }
    }
}
