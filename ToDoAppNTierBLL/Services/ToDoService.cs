using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierBLL.Extensions;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierCommon.ResponseObjects;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierDTO;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierBLL.Services
{
    public class ToDoService : Service<ToDoCreateDto, ToDoUpdateDto, ToDoListDto, ToDo>, IToDoService
        
    {
        private readonly IUnitOfWork _uow;
        /// <summary>
        /// The response is returned according to the validation control of the argument that comes as a parameter.
        /// </summary>
        /// <param name="toDoCreateDto"></param>
        /// <returns>return Response<ToDoCreateDto>(ResponseType type,toDoCreateDto)></returns>

        //public async Task<IResponse<ToDoCreateDto>> Create(ToDoCreateDto toDoCreateDto)
        //{
        //    var validationResult = _createDtoValidator.Validate(toDoCreateDto);
        //    if (validationResult.IsValid)
        //    {
        //        await _unitOfWork.GetRepository<ToDo>().Create(_mapper.Map<ToDo>(toDoCreateDto));
        //        await _unitOfWork.SaveChanges();
        //        return new Response<ToDoCreateDto>(ResponseType.Success, toDoCreateDto);
        //    }
        //    else
        //    {
        //        return new Response<ToDoCreateDto>(ResponseType.ValidationError, toDoCreateDto, validationResult.ConvertToCustomValidationError());
        //    }
        //}
        ///// <summary>
        /////  After converting the data type list obtained with the repository into a data transfer object, the response is returned.
        ///// </summary>
        ///// <returns>return Response<List<ToDoCreateDto>>(ResponseType type,toDoList)</returns>

        //public async Task<IResponse<List<ToDoListDto>>> GetAll()
        //{
        //    var toDoList = _mapper.Map<List<ToDoListDto>>(await _unitOfWork.GetRepository<ToDo>().GetAll());
        //    return new Response<List<ToDoListDto>>(ResponseType.Success, toDoList);

        //}
        ///// <summary>
        ///// According to the incoming id parameter, the response is returned after converting
        ///// the data from the repository into the desired data transfer object.
        ///// </summary>
        ///// <typeparam name="IDto"></typeparam>
        ///// <param name="id"></param>
        ///// <returns>Response<List<ToDoCreateDto>>(ResponseType.type,string message) or Response<List<ToDoCreateDto>>(ResponseType.type,dto)</returns>
        //public async Task<IResponse<IDto>> GetById<IDto>(int id)
        //{
        //    var data = _mapper.Map<IDto>(await _unitOfWork.GetRepository<ToDo>().GetByFilter(x => x.Id == id));
        //    if (data == null)
        //    {
        //        return new Response<IDto>(ResponseType.NotFound, $"{id} related data couldn't not found");
        //    }
        //    else
        //    {
        //        return new Response<IDto>(ResponseType.Success, data);
        //    }
        //}

        //public async Task<IResponse<List<ToDoListDto>>> GetFilteredList(Expression<Func<ToDo, bool>> filter)
        //{
        //    var filteredList = _mapper.Map<List<ToDoListDto>>(await _unitOfWork.GetRepository<ToDo>().GetFilteredList(filter));
        //    return new Response<List<ToDoListDto>>(ResponseType.Success, filteredList);
        //}

        ///// <summary>
        ///// According to the incoming id parameter,
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<IResponse> Remove(int id)
        //{
        //    var removedEntity = await _unitOfWork.GetRepository<ToDo>().GetByFilter(x => x.Id == id);
        //    if (removedEntity != null)
        //    {
        //        _unitOfWork.GetRepository<ToDo>().Remove(removedEntity);
        //        await _unitOfWork.SaveChanges();
        //        return new Response(ResponseType.Success);
        //    }
        //    else
        //    {
        //        return new Response(ResponseType.NotFound, $"{id} related data couldn't found!");
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="toDoUpdateDto"></param>
        ///// <returns></returns>

        //public async Task<IResponse<ToDoUpdateDto>> Update(ToDoUpdateDto toDoUpdateDto)
        //{
        //    var result = _updateDtoValidator.Validate(toDoUpdateDto);
        //    if (result.IsValid)
        //    {
        //        var updatedEntity = await _unitOfWork.GetRepository<ToDo>().Find(toDoUpdateDto.Id);
        //        if (updatedEntity != null)
        //        {
        //            _unitOfWork.GetRepository<ToDo>().Update(_mapper.Map<ToDo>(toDoUpdateDto), updatedEntity);
        //            await _unitOfWork.SaveChanges();
        //            return new Response<ToDoUpdateDto>(ResponseType.Success, toDoUpdateDto);
        //        }
        //        else
        //        {
        //            return new Response<ToDoUpdateDto>(ResponseType.NotFound, $"{ toDoUpdateDto.Id } related data couldn't not found");
        //        }
        //    }
        //    else
        //    {
        //        return new Response<ToDoUpdateDto>(ResponseType.ValidationError, toDoUpdateDto, result.ConvertToCustomValidationError());
        //    }
        //}
        //public int GetToDoCount(Expression<Func<ToDo, bool>> filter)
        //{
        //    return _uow.GetRepository<ToDo>().GetQuery().Count(filter);
        //}

        public ToDoService(IMapper mapper, IValidator<ToDoCreateDto> createDtoValidator, IValidator<ToDoUpdateDto> updateDtoValidator, IUnitOfWork uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
        }

        public int GetToDoCount(Expression<Func<ToDo, bool>> filter)
        {
            return _uow.GetRepository<ToDo>().GetQuery().Count(filter);
        }
    }
}
