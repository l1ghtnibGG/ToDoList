using System;
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

        public ToDoList AddItem(ToDoList item)
        {
            if (item == null)
                return null;
            
            item.CreateDate = DateTime.Now;

            _context.Add(item);
            _context.SaveChanges();

            return item;
        }

        public ToDoList UpdateItem(ToDoList item)
        {
            var id = _context.ToDoLists.FirstOrDefault(x => x.Id == item.Id).Id;

            if (id <= 0)
                return null;
            
            _context.ToDoLists.Update(item);
            _context.SaveChanges();

            return item;
        }

        public long DeleteItem(long id)
        {
            var item = _context.ToDoLists.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return 0;
            
            _context.ToDoLists.Remove(item);
            _context.SaveChanges();

            return id;
        }

        public long UpdateDoneItem(long id)
        {
            var item = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
            
            if (item == null)
                return 0;

            item.IsDone = !item.IsDone;
            item.DueDate = DateTime.Now;
            UpdateItem(item);

            return id;
        }
    }
}
