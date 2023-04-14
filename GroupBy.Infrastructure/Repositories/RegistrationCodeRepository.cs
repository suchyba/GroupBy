using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RegistrationCodeRepository : AsyncRepository<RegistrationCode>, IRegistrationCodeRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IRankRepository rankRepository;
        private readonly IVolunteerRepository volunteerRepository;

        public RegistrationCodeRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IVolunteerRepository volunteerRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
            this.rankRepository = rankRepository;
            this.volunteerRepository = volunteerRepository;
        }

        public override Task<RegistrationCode> UpdateAsync(RegistrationCode domain)
        {
            throw new BadRequestException("Operation not allowed");
        }
        public override async Task<RegistrationCode> CreateAsync(RegistrationCode domain)
        {
            domain.TargetGroup = await groupRepository.GetAsync(domain.TargetGroup);
            if (domain.TargetRank != null)
                domain.TargetRank = await rankRepository.GetAsync(domain.TargetRank);
            domain.Owner = await volunteerRepository.GetAsync(domain.Owner);

            if (!await groupRepository.IsMember(domain.TargetGroup.Id, domain.Owner.Id))
                throw new BadRequestException("Creator of registration code must be a member of the target group");

            var newCode = await context.Set<RegistrationCode>().AddAsync(domain);

            return newCode.Entity;
        }
    }
}
