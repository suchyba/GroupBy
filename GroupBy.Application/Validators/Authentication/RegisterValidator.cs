using FluentValidation;
using GroupBy.Design.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Authentication
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} must be in valid email address format");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(v => v.RelatedVolunteerFirstNames)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.RelatedVolunteerLastName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(v => v.RelatedVolunteerBirthDate)
                .LessThan(DateTime.Today).WithMessage("{PropertyName} has to be the past date.");
            RuleFor(v => v.RelatedVolunteerPhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .When(v => v.RelatedVolunteerPhoneNumber != null && v.RelatedVolunteerPhoneNumber.Any())
                .WithMessage("{PropertyName} has to be correct phone number format.");
            RuleFor(v => v.Password)
                .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 charactest long")
                .Matches("[0-9]").WithMessage("{PropertyName} must contain a digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain non alphanumeric character");
        }
    }
}
