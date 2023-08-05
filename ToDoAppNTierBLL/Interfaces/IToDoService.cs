using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierCommon.ResponseObjects;
using ToDoAppNTierDTO;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierBLL.Interfaces
{
    public interface IToDoService:IService<ToDoCreateDto, ToDoUpdateDto,ToDoListDto,ToDo>
    {
  
        int GetToDoCount(Expression<Func<ToDo, bool>> filter);



    }
}
