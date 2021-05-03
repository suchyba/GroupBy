using FluentValidation;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
{
    public class VolunteerValidator : AbstractValidator<VolunteerViewModel>
    {
        public VolunteerValidator()
        {
            RuleFor(v => v.FirstNames)
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");
            RuleFor(v => v.BirthDate)
                .LessThan(DateTime.Now).WithMessage("{PropertyName} must be smaller than now.");
            RuleFor(v => v.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");
        }
    }
}
