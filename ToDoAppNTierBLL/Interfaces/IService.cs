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
    public interface IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class,IDto,new()
        where UpdateDto: class,IUpdateDto,new()
        where ListDto: class,IDto,new()
        where T:BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<List<ListDto>>> GetAllAsync();
        Task<IResponse<List<ListDto>>> GetFilteredList(Expression<Func<T, bool>> filter);

        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);

        Task<IResponse> Remove(int id);

        Task<IResponse<UpdateDto>> Update(UpdateDto UpdateDto);
    }
}
