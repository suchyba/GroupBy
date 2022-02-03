using FluentValidation;
using GroupBy.Application.DTO.AccountingDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.AccountingDocument
{
    public class AccountingDocumentValidator : AbstractValidator<AccountingDocumentSimpleDTO>
    {
        public AccountingDocumentValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(d => d.FilePath)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
