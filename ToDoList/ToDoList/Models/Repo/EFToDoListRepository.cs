using System.Linq;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models.Repo
{
    public class EFToDoListRepository : IToDoListRepository
{
        private readonly ToDoListDbContext _context;

        public EFToDoListRepository(ToDoListDbContext context)
        {
            _context = context;
        }

        public IQueryable<ToDoList> ToDoLists => _context.ToDoLists;
    }
}
