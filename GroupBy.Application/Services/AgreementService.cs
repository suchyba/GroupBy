using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Validators.Agreement;
using GroupBy.Application.DTO.Agreement;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AgreementService : AsyncService<Agreement, AgreementDTO, AgreementDTO, AgreementCreateDTO, AgreementDTO>, IAgreementService
    {
        public AgreementService(IAgreementRepository repository, IMapper mapper, 
            IValidator<AgreementDTO> validator, IValidator<AgreementCreateDTO> createValidator) 
            : base(repository, mapper, validator, createValidator)
        {

        }
    }
}
