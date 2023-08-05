using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDTO;

namespace ToDoAppNTierDTO
{
   public class ToDoUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Definition{ get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public string Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
