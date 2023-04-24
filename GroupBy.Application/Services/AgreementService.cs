using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Agreement;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class AgreementService : AsyncService<Agreement, AgreementDTO, AgreementDTO, AgreementCreateDTO, AgreementDTO>, IAgreementService
    {
        public AgreementService(
            IAgreementRepository repository,
            IMapper mapper,
            IValidator<AgreementDTO> validator,
            IValidator<AgreementCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, validator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
