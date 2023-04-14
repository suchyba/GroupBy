using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Resolution;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class ResolutionService : AsyncService<Resolution, ResolutionDTO, ResolutionDTO, ResolutionCreateDTO, ResolutionUpdateDTO>, IResolutionService
    {
        public ResolutionService(
            IResolutionRepository repository,
            IMapper mapper,
            IValidator<ResolutionUpdateDTO> updateValidator,
            IValidator<ResolutionCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
