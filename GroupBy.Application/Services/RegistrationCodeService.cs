using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.RegistrationCode;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class RegistrationCodeService : AsyncService<RegistrationCode, RegistrationCodeSimpleDTO, RegistrationCodeFullDTO, RegistrationCodeCreateDTO, RegistrationCodeUpdateDTO>, IRegistrationCodeService
    {
        public RegistrationCodeService(
            IRegistrationCodeRepository repository,
            IMapper mapper,
            IValidator<RegistrationCodeUpdateDTO> updateValidator,
            IValidator<RegistrationCodeCreateDTO> createValidator)
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
        public override async Task<RegistrationCodeFullDTO> CreateAsync(RegistrationCodeCreateDTO model)
        {
            string code = model.GetHashCode().ToString("X6");

            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            var entity = mapper.Map<RegistrationCode>(model);

            entity.Code = code;
            var domain = await repository.CreateAsync(entity);

            return mapper.Map<RegistrationCodeFullDTO>(domain);
        }
    }
}
