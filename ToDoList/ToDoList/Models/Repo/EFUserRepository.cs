using System.Linq;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models.Repo
{
    public class EFUserRepository : IUserRepository
    {
        private readonly ToDoListDbContext _context;

        public EFUserRepository(ToDoListDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;
        
        
        public User AddUser(User userRegistration)
        {
            var user = new User
                {
                    Email = userRegistration.Email,
                    Name = userRegistration.Name,
                    Password = userRegistration.Password
                };

                _context.Add(user);
                _context.SaveChanges();

                return user;
        }
    }
}