using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.RegistrationCode;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class RegistrationCodeService : AsyncService<RegistrationCode, RegistrationCodeSimpleDTO, RegistrationCodeFullDTO, RegistrationCodeCreateDTO, RegistrationCodeUpdateDTO>, IRegistrationCodeService
    {
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IRankRepository rankRepository;

        public RegistrationCodeService(
            IRegistrationCodeRepository repository,
            IVolunteerRepository volunteerRepository,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IMapper mapper,
            IValidator<RegistrationCodeUpdateDTO> updateValidator,
            IValidator<RegistrationCodeCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.volunteerRepository = volunteerRepository;
            this.groupRepository = groupRepository;
            this.rankRepository = rankRepository;
        }

        public override async Task<RegistrationCodeFullDTO> GetAsync(RegistrationCodeSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<RegistrationCodeFullDTO>(await repository.GetAsync(mapper.Map<RegistrationCode>(model), includes: new string[] { "TargetGroup", "TargetRank", "Owner" }));
            }
        }

        protected override async Task<RegistrationCode> CreateOperationAsync(RegistrationCode entity)
        {
            string code = entity.GetHashCode().ToString("X6");

            entity.Code = code;
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.TargetGroup = await groupRepository.GetAsync(entity.TargetGroup);
                if (entity.TargetRank != null)
                    entity.TargetRank = await rankRepository.GetAsync(entity.TargetRank);
                entity.Owner = await volunteerRepository.GetAsync(entity.Owner);

                if (!await groupRepository.IsMember(entity.TargetGroup.Id, entity.Owner))
                    throw new BadRequestException("Creator of registration code must be a member of the target group");

                var createdCode = await repository.CreateAsync(entity);
                await uow.Commit();

                return createdCode;
            }
        }
    }
}
