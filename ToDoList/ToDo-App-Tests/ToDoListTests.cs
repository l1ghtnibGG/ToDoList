using Moq;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo_Domain_Entities;
using ToDoList_Mvc_UI.Controllers;
using ToDoList_Mvc_UI.Models.Repo;
using Xunit;
using Assert = Xunit.Assert;
using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace ToDo_App_Tests
{
    public class ToDoListTests
    {
        [Fact]
        public void Index_ReturnAViewResult_WithAllTasks()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            mockToDoList.Setup(repo => repo.ToDoLists).Returns(new ToDoList[]
                    {
                        new ToDoList
                        {   
                            Id = 1,
                            Name = "Running",
                            IsDone = true,
                            CreateDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            Description = "running for 10min", 
                            UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
                        },
                        new ToDoList
                        {
                            Id = 2,
                            Name = "Swimming",
                            IsDone = false,
                            CreateDate = DateTime.Now.AddDays(1),
                            DueDate = DateTime.Now.AddDays(5),
                            Description = "swimming once per day",
                            UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
                        },
                        new ToDoList
                        {
                            Id = 3,
                            Name = "Reading",
                            IsDone = false,
                            CreateDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            Description = "",
                            UserId = new Guid("57b4bb4a-38db-447f-a5ab-c857da51198c")
                        }
                      }.AsQueryable());
            
            var mockUser = new Mock<IUserRepository>();

            var controller = new HomeController(mockToDoList.Object, mockUser.Object);

            var result = controller.Index(new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")) as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<ToDoList>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
            mockToDoList.Verify();
        }

        [Fact]
        public void AddItem_RedirectToIndex()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            var mockUser = new Mock<IUserRepository>();

            mockToDoList.Setup(repo => repo.AddItem(It.IsAny<ToDoList>()))
                .Returns<ToDoList>(arg => arg);

            var controller = new HomeController(mockToDoList.Object, mockUser.Object);
            var newTask = new ToDoList
            {
                Name = "Dancing",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "learn dancing",
                UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
            };

            var result = controller.AddItem(newTask.UserId.ToString(), newTask);

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }

        [Fact]
        public void AddItem_BadRequest()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            mockToDoList.Setup(repo => repo.AddItem(It.IsAny<ToDoList>())).Verifiable();

            var mockUser = new Mock<IUserRepository>();

            var controller = new HomeController(mockToDoList.Object, mockUser.Object);
            var newTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min",
                UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
            };

            var result = controller.AddItem(newTask.UserId.ToString(), newTask) ;

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }

        [Fact]
        public void UpdateItem_RedirectToIndex()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            var mockUser = new Mock<IUserRepository>();

            mockToDoList.Setup(repo => repo.UpdateItem(It.IsAny<ToDoList>()))
                .Returns<ToDoList>(arg => arg);
            
            var controller = new HomeController(mockToDoList.Object, mockUser.Object);   
            var editTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min",
                UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
            };

            var result = controller.UpdateItem(editTask.UserId.ToString(), editTask);

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }
        
        [Fact]
        public void UpdateItem_BadRequest()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            mockToDoList.Setup(repo => repo.UpdateItem(It.IsAny<ToDoList>())).Verifiable();

            var mockUser = new Mock<IUserRepository>();
            mockUser.Setup(repo => repo.AddUser(It.IsAny<User>())).Verifiable();
            
            var controller = new HomeController(mockToDoList.Object, mockUser.Object); 
            var newTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min",
                UserId = new Guid("bb3e98af-5558-4a79-abed-f3bf847870f5")
            };

            var result = controller.UpdateItem(newTask.UserId.ToString(), newTask);

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }
        
        [Fact]
        public void DeleteItem_RedirectToIndex()
        {
            var mockToDoList = new Mock<IToDoListRepository>();
            var mockUser = new Mock<IUserRepository>();

            mockToDoList.Setup(repo => repo.DeleteItem(It.IsAny<long>()))
                .Returns(1);

            var controller = new HomeController(mockToDoList.Object, mockUser.Object);
            var deleteId = (long)1;

            var result = controller.DeleteItem(deleteId,"bb3e98af-5558-4a79-abed-f3bf847870f5");

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }

        [Fact]
        public void ToggleIsDone_RedirectToIndex()
        { 
            var mockToDoList = new Mock<IToDoListRepository>();
            var mockUser = new Mock<IUserRepository>();

            mockToDoList.Setup(repo => repo.UpdateDoneItem(It.IsAny<long>()))
                .Returns<long>(arg => arg);

            var controller = new HomeController(mockToDoList.Object, mockUser.Object);
            var toggleItem = (long)1;

            var result = controller.ToggleIsDone(toggleItem, "bb3e98af-5558-4a79-abed-f3bf847870f5");

            var redirectToActionResult = 
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockToDoList.Verify();
        }
    }
}
