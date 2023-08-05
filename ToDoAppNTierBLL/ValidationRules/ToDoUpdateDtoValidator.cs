using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDTO;

namespace ToDoAppNTierBLL.ValidationRules
{
    public class ToDoUpdateDtoValidator: AbstractValidator<ToDoUpdateDto>
    {
        public ToDoUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Priority).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please give a start date!").When(x => x.IsStarted);
            RuleFor(x => x.FinishDate).NotEmpty().WithMessage("Please give a finish date!").When(x => x.IsCompleted);
            RuleFor(x => x.IsStarted).NotEmpty().WithMessage("Please be sure that your mission has started!").When(x => x.IsCompleted);

        }

    }
}
