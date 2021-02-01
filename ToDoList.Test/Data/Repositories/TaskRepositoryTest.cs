using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CanUpdateValidTask_ReturnTask() 
        {
            // Arrange
            var taskEntity = new TaskEntity
            {
                Id = Guid.NewGuid().ToString(),
                Completeness = false,
                DateTime = DateTime.Now,
                Description = "Description",
                Name = "Name",
            };

            var taskEntityUPDATE = new TaskEntity
            {
                Id = taskEntity.Id,
                Completeness = true,
                DateTime = DateTime.Now,
                Description = "Description 2",
                Name = "Name 2",
            };

            ITaskRepository taskRepository = new TaskRepository();
            taskRepository.AddTask(taskEntity);

            // Act
            var result = taskRepository.UpdateTask(taskEntityUPDATE);
            taskRepository.DeleteTaskById(taskEntityUPDATE.Id);

            // Assert
            Assert.AreEqual(taskEntity, result);
            Assert.AreEqual(taskEntityUPDATE.Name, result.Name);
            Assert.AreEqual(taskEntityUPDATE.Description, result.Description);
            Assert.AreEqual(taskEntityUPDATE.DateTime, result.DateTime);
            Assert.AreEqual(taskEntityUPDATE.Id, result.Id);
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

        [TestMethod]
        public void CanGetTasksByName()
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
            taskRepository.AddTask(taskEntity);

            // Act
            var result = taskRepository.GetTasksByName(taskEntity.Name);
            
            // Assert
            Assert.AreEqual(taskEntity, result.FirstOrDefault(t => t.Name == taskEntity.Name));

            taskRepository.DeleteTaskById(taskEntity.Id);
        }
    }
}
