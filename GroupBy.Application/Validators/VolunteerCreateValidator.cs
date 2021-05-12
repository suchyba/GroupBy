using FluentValidation;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
{
    public class VolunteerCreateValidator : AbstractValidator<VolunteerCreateViewModel>
    {
        public VolunteerCreateValidator()
        {
            RuleFor(v => v.FirstNames)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.BirthDate)
                .GreaterThan(DateTime.Today).WithMessage("{PropertyName} has to be the past date.");
            RuleFor(v => v.RankId)
                .GreaterThan(0).When(v => v.RankId != null).WithMessage("{PropertyName} has to be greater than 0.");
            RuleFor(v => v.PhoneNumber)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .When(v => v.PhoneNumber != null && v.PhoneNumber.Any())
                .WithMessage("{PropertyName} has to be correct phone number format.");
        }
    }
}
