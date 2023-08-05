using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierCommon.ResponseObjects;
using ToDoAppNTierDTO;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierBLL.Interfaces
{
    public interface IUserService:IService<UserRegisterDto,UserEditDto,UserListDto,User>
    {
        Task<IResponse<UserRegisterDto>> Register(UserRegisterDto dto);
        Task<IResponse<UserEditDto>> ProfileEdit(UserEditDto dto);
    }
}
