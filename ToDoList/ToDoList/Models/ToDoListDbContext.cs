using Microsoft.EntityFrameworkCore;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
            : base(options) { }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
