using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class UserService : Service<UserRegisterDto, UserEditDto, UserListDto, User>, IUserService
    {
        private readonly IValidator<UserRegisterDto> _createDtoValidator;
        private readonly IValidator<UserEditDto> _updateDtoValidator;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IValidator<UserRegisterDto> createDtoValidator, IValidator<UserEditDto> updateDtoValidator, IUnitOfWork uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse<UserRegisterDto>> Register(UserRegisterDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                if (!_uow.GetRepository<User>().IsThereOrNot(x => x.Email.Equals(dto.Email)))
                {
                    if (!_uow.GetRepository<User>().IsThereOrNot(x => x.UserName.Equals(dto.UserName)))
                    {

                        await _uow.GetRepository<User>().Create(_mapper.Map<User>(dto));
                        await _uow.SaveChanges();
                        return new Response<UserRegisterDto>(ResponseType.Success, dto, "You have been successfully registered!");
                    }
                    List<CustomValidationError> error = new List<CustomValidationError>
                    {
                        new CustomValidationError{ErrorMessage="This username is already used by another user", PropertyName=""}
                    };
                    return new Response<UserRegisterDto>(ResponseType.ValidationError, dto, error);
                }
                List<CustomValidationError> errors = new List<CustomValidationError>
                {
                    new CustomValidationError{ ErrorMessage="You have already registered with this email address ", PropertyName=""}
                };
                return new Response<UserRegisterDto>(ResponseType.ValidationError, dto, errors);
            }
            return new Response<UserRegisterDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<UserEditDto>> ProfileEdit(UserEditDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
              
             List<User> users = await _uow.GetRepository<User>().GetFilteredList(x =>x.Id != dto.Id);   
 
                if (!users.Any(x => x.UserName.Equals(dto.UserName)))
                {
                    var updatedEntity = await _uow.GetRepository<User>().Find(dto.Id);
                    if (updatedEntity != null)
                    {
                        _uow.GetRepository<User>().Update(_mapper.Map<User>(dto), updatedEntity);
                        await _uow.SaveChanges();
                        return new Response<UserEditDto>(ResponseType.Success, dto,"Your profile information have been successfully updated!");
                    }
                    return new Response<UserEditDto>(ResponseType.NotFound, $"{dto.Id} related data couldn't not found");
                }
                List<CustomValidationError> error = new List<CustomValidationError>
                 {
                        new CustomValidationError{ErrorMessage="This username is already used by another user", PropertyName=""}
                 };
                return new Response<UserEditDto>(ResponseType.ValidationError, dto, error);

            }
            return new Response<UserEditDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
        }




    }
}
