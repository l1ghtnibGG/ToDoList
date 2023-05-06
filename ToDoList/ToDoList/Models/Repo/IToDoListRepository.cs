using System.Linq;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models.Repo
{
    public interface IToDoListRepository
    {
        public IQueryable<ToDoList> ToDoLists { get; }

        public ToDoList AddItem(ToDoList item);

        public ToDoList UpdateItem(ToDoList item);

        public long DeleteItem(long id);

        public long UpdateDoneItem(long id);
    }
}
