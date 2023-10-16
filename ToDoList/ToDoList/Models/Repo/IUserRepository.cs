using System.Linq;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models.Repo
{
    public interface IUserRepository
    {
        public IQueryable<User> Users { get; }

        public User AddUser(User user);
    }
}