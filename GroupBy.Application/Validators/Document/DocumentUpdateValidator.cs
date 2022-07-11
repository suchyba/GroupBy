using FluentValidation;
using GroupBy.Application.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Document
{
    public class DocumentUpdateValidator : AbstractValidator<DocumentUpdateDTO>
    {
        public DocumentUpdateValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(d => d.RelatedProjectId)
                .GreaterThan(0)
                .When(d => d.RelatedProjectId != null)
                .WithMessage("{PropertyName} has to be greater then 0.");
            RuleFor(d => d.FilePath)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
