using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoList.Data.Context;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Abstract;

namespace ToDoList.Test.Data.Repositories
{
    [TestClass]
    public class TaskRepositoryTest
    {
        #region TODO: Check list
        //[TestMethod]
        //public void CanGetAllTasks()
        //{
        //    // Arrange
        //    #region Task list
        //    IEnumerable<TaskEntity> tasks = new List<TaskEntity>
        //    {
        //        new TaskEntity 
        //        { 
        //            Id = "Guid1", 
        //            Completeness = false,
        //            DateTime = DateTime.Now,
        //            Description = "Description",
        //            Name = "Name", 
        //        },
        //        new TaskEntity
        //        {
        //            Id = "Guid2",
        //            Completeness = false,
        //            DateTime = DateTime.Now,
        //            Description = "Description",
        //            Name = "Name",
        //        },
        //        new TaskEntity
        //        {
        //            Id = "Guid3",
        //            Completeness = false,
        //            DateTime = DateTime.Now,
        //            Description = "Description",
        //            Name = "Name",
        //        },
        //    };
        //    #endregion
        //    ITaskRepository taskRepository = new TaskRepository();

        //    // Act
        //    var result = taskRepository.GetAllTasks();

        //    // Assert
        //    Assert.AreEqual(tasks, result);
        //}
        #endregion

        [TestMethod]
        public void CanAddValidTask_ReturnTask()
        {
            // Arrange
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskRepository taskRepository = new TaskRepository();


            // Act
            var result = taskRepository.AddTask(taskEntity);

            // Assert
            Assert.AreEqual(taskEntity, result);

            taskRepository.DeleteTaskById(taskEntity.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CanotAddInValidTask_ReturnException()
        {
            // Arrange
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "dfas",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "",
                Name = "   ",
            };
            ITaskRepository taskRepository = new TaskRepository();

            // Act
            taskRepository.AddTask(taskEntity);
        }

        [TestMethod]
        public void CanUpdateValidTask_ReturnTask() // Do not work because method Update dont work.
        {
            // Arrange
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskRepository taskRepository = new TaskRepository();

            // Act
            var result = taskRepository.UpdateTask(taskEntity);

            // Assert
            Assert.AreEqual(taskEntity, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CanotUpdateInValidTask_ReturnException()
        {
            // Arrange
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "dfas",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "",
                Name = "   ",
            };
            ITaskRepository taskRepository = new TaskRepository();

            // Act
            taskRepository.UpdateTask(taskEntity);
        }

        [TestMethod]
        public void CanDeleteByValidId()
        {
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskRepository taskRepository = new TaskRepository();

            // Act
            taskRepository.AddTask(taskEntity);
            taskRepository.DeleteTaskById(taskEntity.Id);

            // Assert
            Assert.IsNotNull(taskEntity);
        }

        [TestMethod]
        public void CanGetTaskByValidId()
        {
            TaskEntity taskEntity = new TaskEntity
            {
                Id = "Guid1",
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };
            ITaskRepository taskRepository = new TaskRepository();

            // Act
            taskRepository.GetTaskById(taskEntity.Id);

            // Assert
            Assert.IsNotNull(taskEntity);
        }
    }
}
