using FluentValidation;
using GroupBy.Application.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Document
{
    public class DocumentCreateValidator : AbstractValidator<DocumentCreateDTO>
    {
        public DocumentCreateValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(d => d.GroupsId)
                .NotNull()
                .WithMessage("{PropertyName} is required");
            RuleFor(d => d.GroupsId)
                .Must(ids => ids?.Count() > 0)
                .WithMessage("Document must be related to at least 1 group");
            RuleFor(d => d.RelatedProjectId)
                .GreaterThan(0).When(d => d.RelatedProjectId != null).WithMessage("{PropertyName} has to be greater then 0.");
            RuleFor(d => d.FilePath)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
