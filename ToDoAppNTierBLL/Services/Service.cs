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
    public class Service<CreateDto, UpdateDto, ListDto, T>: IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class,IUpdateDto,new()
        where ListDto: class,IDto,new()
        where T:BaseEntity

    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUnitOfWork _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUnitOfWork uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result= _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createEntity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().Create(createEntity);
                await _uow.SaveChanges();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(ResponseType.ValidationError,dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAll();
            var dtos = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dtos);
        }

        public async Task<IResponse<List<ListDto>>> GetFilteredList(Expression<Func<T, bool>> filter)
        {
            var filteredList = _mapper.Map<List<ListDto>>(await _uow.GetRepository<T>().GetFilteredList(filter));
            return new Response<List<ListDto>>(ResponseType.Success, filteredList);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var entity = await _uow.GetRepository<T>().GetByFilter(x => x.Id == id);
            if (entity == null)
                return new Response<IDto>(ResponseType.NotFound, $"Data which has {id} cannot be found!");
            var dto = _mapper.Map<IDto>(entity);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<T>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<T>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} related data couldn't found!");
            }
        }

        public async Task<IResponse<UpdateDto>> Update(UpdateDto UpdateDto)
        {
            var result = _updateDtoValidator.Validate(UpdateDto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<T>().Find(UpdateDto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<T>().Update(_mapper.Map<T>(UpdateDto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<UpdateDto>(ResponseType.Success,UpdateDto);
                }
                else
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{UpdateDto.Id} related data couldn't not found");
                }
            }
            else
            {
                return new Response<UpdateDto>(ResponseType.ValidationError, UpdateDto, result.ConvertToCustomValidationError());
            }
        }
    }
}
