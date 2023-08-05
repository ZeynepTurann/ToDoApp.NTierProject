using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ToDoAppNTierDTO;

namespace ToDoAppNTierBLL.ValidationRules
{
    public class ToDoCreateDtoValidator:AbstractValidator<ToDoCreateDto>
    {
        public ToDoCreateDtoValidator()
        { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Priority).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start Time Is Required!").When(x => x.IsStarted);

            

        }

    }
}
