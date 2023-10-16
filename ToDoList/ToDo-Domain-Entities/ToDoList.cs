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

        public DateTime CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }
        
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
