using FluentValidation;
using GroupBy.Design.TO.AccountingDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.AccountingDocument
{
    public class AccountingDocumentCreateValidator : AbstractValidator<AccountingDocumentCreateDTO>
    {
        public AccountingDocumentCreateValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(d => d.FilePath)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(d => d.GroupsId)
                .NotNull()
                .WithMessage("{PropertyName} is required");
            RuleFor(d => d.GroupsId)
                .Must(ids => ids?.Count() > 0)
                .WithMessage("Accounting document must be related to at least 1 group");
            RuleFor(d => d.RelatedProjectId)
                .NotEmpty().When(d => d.RelatedProjectId.HasValue)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
