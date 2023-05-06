using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo_Domain_Entities;
using ToDoList_Mvc_UI.Controllers;
using ToDoList_Mvc_UI.Models.Repo;
using Xunit;
using Assert = Xunit.Assert;

namespace ToDo_App_Tests
{
    public class ToDoListTests
    {
        [Fact]
        public void Index_ReturnAViewResult_WithAllTasks()
        {
            var mock = new Mock<IToDoListRepository>();
            mock.Setup(repo => repo.ToDoLists).Returns(new ToDoList[]
                    {
                        new ToDoList
                        {   
                            Id = 1,
                            Name = "Running",
                            IsDone = true,
                            CreateDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            Description = "running for 10min"
                        },
                        new ToDoList
                        {
                            Id = 2,
                            Name = "Swimming",
                            IsDone = false,
                            CreateDate = DateTime.Now.AddDays(1),
                            DueDate = DateTime.Now.AddDays(5),
                            Description = "swimming once per day"
                        },
                        new ToDoList
                        {
                            Id = 3,
                            Name = "Reading",
                            IsDone = false,
                            CreateDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            Description = ""
                        }
                      }.AsQueryable());

            var controller = new HomeController(mock.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<ToDoList>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void AddItem_RedirectToIndex()
        {
            var mock = new Mock<IToDoListRepository>();
            mock.Setup(repo => repo.AddItem(It.IsAny<ToDoList>())).Verifiable();
            var controller = new HomeController(mock.Object);
            var newTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min"
            };

            var result = controller.AddItem(newTask);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect.ControllerName);
            Assert.Equal("Index", redirect.ActionName);
            mock.Verify();
        }

        [Fact]
        public void AddItem_BadRequest()
        {
            var mock = new Mock<IToDoListRepository>();

            var controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var newTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min"
            };

            var result = controller.AddItem(newTask);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequest.Value);
        }

        [Fact]
        public void UpdateItem_RedirectToIndex()
        {
            var mock = new Mock<IToDoListRepository>();
            mock.Setup(repo => repo.UpdateItem(It.IsAny<ToDoList>())).Verifiable();
            var controller = new HomeController(mock.Object);   
            var editTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min"
            };

            var result = controller.UpdateItem(editTask);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect.ControllerName);
            Assert.Equal("Index", redirect.ActionName);
            mock.Verify();
        }
        
        [Fact]
        public void UpdateItem_BadRequest()
        {
            var mock = new Mock<IToDoListRepository>();

            var controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var newTask = new ToDoList
            {
                Id = 1,
                Name = "Running",
                IsDone = true,
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                Description = "running for 10min"
            };

            var result = controller.UpdateItem(newTask);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequest.Value);
        }
        
        [Fact]
        public void DeleteItem_RedirectToIndex()
        {
            var mock = new Mock<IToDoListRepository>();
            mock.Setup(repo => repo.DeleteItem(It.IsAny<long>())).Verifiable();
            var controller = new HomeController(mock.Object);
            var deleteId = (long)11;

            var result = controller.DeleteItem(deleteId);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect.ControllerName);
            Assert.Equal("Index", redirect.ActionName);
            mock.Verify();
        }

        [Fact]
        public void ToggleIsDone_RedirectToIndex()
        {
            var mock = new Mock<IToDoListRepository>();
            mock.Setup(repo => repo.UpdateDoneItem(It.IsAny<long>())).Verifiable();
            var controller = new HomeController(mock.Object);
            var toggleItem = (long)11;

            var result = controller.ToggleIsDone(toggleItem);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect.ControllerName);
            Assert.Equal("Index", redirect.ActionName);
            mock.Verify();
        }
    }
}
