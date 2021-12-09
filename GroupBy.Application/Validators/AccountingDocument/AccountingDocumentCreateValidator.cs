using FluentValidation;
using GroupBy.Application.DTO.AccountingDocument;
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
            RuleFor(d => d.GroupId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater then 0");
            RuleFor(d => d.ProjectId)
                .GreaterThan(0).When(d => d.ProjectId.HasValue)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
