using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.Resolution;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class ResolutionService : AsyncService<Resolution, ResolutionDTO, ResolutionDTO, ResolutionCreateDTO, ResolutionUpdateDTO>, IResolutionService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IVolunteerRepository volunteerRepository;

        public ResolutionService(
            IResolutionRepository repository,
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository,
            IMapper mapper,
            IValidator<ResolutionUpdateDTO> updateValidator,
            IValidator<ResolutionCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
            this.volunteerRepository = volunteerRepository;
        }

        public override async Task<ResolutionDTO> GetAsync(ResolutionDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<ResolutionDTO>(await repository.GetAsync(mapper.Map<Resolution>(model), includeLocal: false, includes: new string[] { "Legislator", "Group" }));
            }
        }

        protected override async Task<Resolution> CreateOperationAsync(ResolutionCreateDTO model)
        {
            var entity = mapper.Map<Resolution>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Group = await groupRepository.GetAsync(entity.Group);
                entity.Legislator = await volunteerRepository.GetAsync(entity.Legislator);

                if (!await groupRepository.IsMember(entity.Group.Id, entity.Legislator))
                    throw new BadRequestException("Legislator must be a member of the group");

                var createdResolution = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdResolution;
            }
        }
    }
}
