using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Rank;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class RankService : AsyncService<Rank, RankSimpleDTO, RankDTO, RankCreateDTO, RankSimpleDTO>, IRankService
    {
        public RankService(
            IRankRepository repository,
            IMapper mapper,
            IValidator<RankSimpleDTO> validator,
            IValidator<RankCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, validator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
