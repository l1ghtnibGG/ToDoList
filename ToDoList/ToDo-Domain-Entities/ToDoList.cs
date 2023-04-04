using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo_Domain_Entities
{
    public class ToDoList
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public bool IsDone { get; set; }
    }
}
