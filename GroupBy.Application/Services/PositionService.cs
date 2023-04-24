using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Position;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class PositionService : AsyncService<Position, PositionSimpleDTO, PositionDTO, PositionCreateDTO, PositionSimpleDTO>, IPositionService
    {
        public PositionService(
            IPositionRepository positionRepository,
            IMapper mapper,
            IValidator<PositionSimpleDTO> updateValidator,
            IValidator<PositionCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(positionRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public override async Task<PositionDTO> GetAsync(PositionSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<PositionDTO>(await repository.GetAsync(mapper.Map<Position>(model), includes: "HigherPosition"));
            }
        }

        protected override async Task<Position> CreateOperationAsync(Position entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.HigherPosition != null)
                    entity.HigherPosition = await repository.GetAsync(entity.HigherPosition);

                var createdPosition = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdPosition;
            }
        }

        protected override async Task<Position> UpdateOperationAsync(Position entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var positionToUpdate = await repository.GetAsync(entity, includes: "HigherPosition");
                positionToUpdate.Name = entity.Name;

                if (entity.HigherPosition != null)
                    entity.HigherPosition = await repository.GetAsync(entity.HigherPosition);

                positionToUpdate.HigherPosition = entity.HigherPosition;

                var updatedPosition = await repository.UpdateAsync(positionToUpdate);
                await uow.Commit();
                return updatedPosition;
            }
        }
    }
}
