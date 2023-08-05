using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDTO;

namespace ToDoAppNTierBLL.ValidationRules
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(20).WithMessage("must create a username");
            RuleFor(x => x.Email).NotEmpty().WithMessage("must type a valid email").EmailAddress();
            RuleFor(x => x.Password)
                          .NotEmpty().WithMessage("must create a password")
                          .MinimumLength(8)
                          .Matches("[A-Z]+").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                          .Matches("[a-z]+").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
                          .Matches(@"(\d)+").WithMessage("'{PropertyName}' must contain one or more digits.");

           
            

        }
    }
}
