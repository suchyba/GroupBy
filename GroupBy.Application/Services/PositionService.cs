using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Position;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

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
    }
}
