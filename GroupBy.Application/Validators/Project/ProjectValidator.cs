using FluentValidation;
using GroupBy.Design.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Project
{
    public class ProjectValidator : AbstractValidator<ProjectSimpleDTO>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.BeginDate)
                .GreaterThan(DateTime.MinValue)
                .When(p => p.BeginDate.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.EndDate)
                .GreaterThan(DateTime.MinValue)
                .When(p => p.EndDate.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
