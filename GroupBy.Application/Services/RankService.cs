using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.Rank;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Threading.Tasks;

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

        public override async Task<RankDTO> GetAsync(RankSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<RankDTO>(await repository.GetAsync(mapper.Map<Rank>(model), includes: "HigherRank"));
            }
        }

        protected override async Task<Rank> CreateOperationAsync(RankCreateDTO model)
        {
            var entity = mapper.Map<Rank>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.HigherRank != null)
                    entity.HigherRank = await repository.GetAsync(entity.HigherRank);

                var createdRank = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdRank;
            }
        }

        protected override async Task<Rank> UpdateOperationAsync(Rank entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.HigherRank != null)
                    entity.HigherRank = await repository.GetAsync(entity.HigherRank);

                var updatedRank = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedRank;
            }
        }
    }
}
