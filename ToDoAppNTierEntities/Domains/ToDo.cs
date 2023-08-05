using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTierEntities.Domains
{
    public class ToDo:BaseEntity
    {
        
        public string Definition { get; set; }
        public bool IsCompleted { get; set; } 
        public bool IsStarted { get; set; } 
        public string Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate  { get; set; }

        public DateTime? FinishDate { get; set; }
        
        public DateTime? Deadline { get; set; }

        //Navigation Props Begin

        public int UserId { get; set; }
        public User User { get; set; }


    }
}
