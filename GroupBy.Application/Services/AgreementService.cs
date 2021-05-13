using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Validators.Agreement;
using GroupBy.Application.ViewModels.Agreement;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AgreementService : AsyncService<Agreement, AgreementViewModel, AgreementCreateViewModel, AgreementViewModel>, IAgreementService
    {
        public AgreementService(IAgreementRepository repository, IMapper mapper, 
            IValidator<AgreementViewModel> validator, IValidator<AgreementCreateViewModel> createValidator) 
            : base(repository, mapper, validator, createValidator)
        {

        }
    }
}
