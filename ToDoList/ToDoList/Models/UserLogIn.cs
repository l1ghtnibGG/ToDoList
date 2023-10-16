using System.ComponentModel.DataAnnotations;

namespace ToDoList_Mvc_UI.Models
{
    public class UserLogIn
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}