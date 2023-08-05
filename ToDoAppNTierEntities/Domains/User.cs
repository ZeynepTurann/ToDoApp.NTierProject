using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTierEntities.Domains
{
   public class User:BaseEntity
    {
        public string UserName { get; set; }    
        public string Password { get; set; }    

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }= DateTime.Now;   
        public string Image { get; set; }

        public IFormFile UploadImage { get; set; }

        public string Occupation { get; set; }  

        //Relational Properties Begin

        public List<ToDo> ToDos  { get; set; }
    }
}
