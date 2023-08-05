using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDTO;

namespace ToDoAppNTierDTO
{
    public class UserListDto:IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string Image { get; set; }

    }
}
