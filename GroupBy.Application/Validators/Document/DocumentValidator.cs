using FluentValidation;
using GroupBy.Design.TO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Document
{
    public class DocumentValidator : AbstractValidator<DocumentDTO>
    {
        public DocumentValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(d => d.FilePath)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
