using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.RegistrationCode;
using GroupBy.Design.UnitOfWork;
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
            IValidator<RegistrationCodeCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
        public override async Task<RegistrationCodeFullDTO> CreateAsync(RegistrationCodeCreateDTO model)
        {
            string code = model.GetHashCode().ToString("X6");

            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            var entity = mapper.Map<RegistrationCode>(model);

            entity.Code = code;
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var domain = await repository.CreateAsync(entity);
                await uow.Commit();

                return mapper.Map<RegistrationCodeFullDTO>(domain);
            }
        }
    }
}
