using System.Linq;
using ToDo_Domain_Entities;

namespace ToDoList_Mvc_UI.Models.Repo
{
    public interface IToDoListRepository
    {
        IQueryable<ToDoList> ToDoLists { get; }
    }
}
