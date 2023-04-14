using FluentValidation;
using GroupBy.Design.TO.Volunteer;
using System;
using System.Linq;

namespace GroupBy.Application.Validators.Volunteer
{
    public class VolunteerUpdateValidator : AbstractValidator<VolunteerUpdateDTO>
    {
        public VolunteerUpdateValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.FirstNames)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.BirthDate)
                .LessThan(DateTime.Today).WithMessage("{PropertyName} has to be the past date.");
            RuleFor(v => v.RankId)
                .NotEmpty().When(v => v.RankId != null).WithMessage("{PropertyName} has to be greater than 0.");
            RuleFor(v => v.PhoneNumber)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .When(v => v.PhoneNumber != null && v.PhoneNumber.Any())
                .WithMessage("{PropertyName} has to be correct phone number format.");
        }
    }
}
