using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
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
                        IsDone = false
                    },
                    new ToDoList
                    {
                        Name = "Wake up",
                        IsDone = true
                    },
                    new ToDoList
                    {
                        Name = "Hit the gym",
                        IsDone = false
                    },
                    new ToDoList
                    {
                        Name = "Reading",
                        IsDone = false
                    });

                context.SaveChanges();
            }
        }
    }
}
