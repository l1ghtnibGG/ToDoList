using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models
{
    public static class SeedData
    {
        public static void EnsureData(IApplicationBuilder app)
        {
            ToDoListDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetService<ToDoListDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "Vlad",
                        Email = "vlad123@mail.ru",
                        Password = "1234",
                        ToDoLists = new List<ToDoList>
                        {
                            new ToDoList
                            {
                                Name = "Run",
                                IsDone = false,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now,
                                Description = "running 20 min"
                            },
                            new ToDoList
                            {
                                Name = "Wake up",
                                IsDone = true,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now.AddDays(1),
                                Description = "wake up at 7am"
                            },
                            new ToDoList
                            {
                                Name = "Hit the gym",
                                IsDone = false,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now,
                                Description = ""
                            },
                            new ToDoList
                            {
                                Name = "Reading",
                                IsDone = true,
                                CreateDate = DateTime.Now.AddDays(1),
                                DueDate = DateTime.Now.AddDays(1),
                                Description = "reading 10 pages"
                            }
                        }
                    },
                    new User
                    {
                       Name = "Vika",
                       Email = "vika123@mail.ru",
                       Password = "1234",
                       ToDoLists = new List<ToDoList>
                       {
                           new ToDoList
                           {
                               Name = "Work",
                               IsDone = false,
                               CreateDate = DateTime.Now,
                               DueDate = DateTime.Now,
                               Description = "work"
                           },
                           new ToDoList
                           {
                               Name = "Wake up",
                               IsDone = true,
                               CreateDate = DateTime.Now,
                               DueDate = DateTime.Now.AddDays(1),
                               Description = "wake up at 7am"
                           },
                           new ToDoList
                           {
                               Name = "Hit the gym",
                               IsDone = false,
                               CreateDate = DateTime.Now,
                               DueDate = DateTime.Now,
                               Description = ""
                           },
                           new ToDoList
                           {
                               Name = "Reading",
                               IsDone = true,
                               CreateDate = DateTime.Now.AddDays(1),
                               DueDate = DateTime.Now.AddDays(1),
                               Description = "reading 10 pages"
                           }
                       }
                    },
                    new User
                    {
                        Name = "Petya",
                        Email = "petya123@mail.ru",
                        Password = "1234",
                        ToDoLists = new List<ToDoList>
                        {
                            new ToDoList
                            {
                                Name = "Go home",
                                IsDone = false,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now,
                                Description = ""
                            },
                            new ToDoList
                            {
                                Name = "Wake up",
                                IsDone = true,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now.AddDays(1),
                                Description = "wake up at 7am"
                            },
                            new ToDoList
                            {
                                Name = "Tv",
                                IsDone = false,
                                CreateDate = DateTime.Now,
                                DueDate = DateTime.Now,
                                Description = ""
                            },
                            new ToDoList
                            {
                                Name = "Reading",
                                IsDone = true,
                                CreateDate = DateTime.Now.AddDays(1),
                                DueDate = DateTime.Now.AddDays(1),
                                Description = "reading 10 pages"
                            }
                        }
                    });
                context.SaveChanges();
            }
        }
    }
}
