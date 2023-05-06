using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            if (!context.ToDoLists.Any())
            {
                context.ToDoLists.AddRange(
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
                    });

                context.SaveChanges();
            }
        }
    }
}
