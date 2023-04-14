using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ResolutionRepository : AsyncRepository<Resolution>, IResolutionRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IVolunteerRepository volunteerRepository;

        public ResolutionRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
            this.volunteerRepository = volunteerRepository;
        }
        public async override Task<Resolution> CreateAsync(Resolution domain)
        {
            domain.Group = await groupRepository.GetAsync(domain.Group, false, "Members");
            domain.Legislator = await volunteerRepository.GetAsync(domain.Legislator);

            if (!domain.Group.Members.Contains(domain.Legislator))
                throw new BadRequestException("Legislator must be a member of the group");

            var resolution = await context.Set<Resolution>().AddAsync(domain);
            return resolution.Entity;
        }

        public async override Task<Resolution> UpdateAsync(Resolution domain)
        {
            Resolution resolution = await GetAsync(domain);
            resolution.Content = domain.Content;
            resolution.Date = domain.Date;
            resolution.Symbol = domain.Symbol;

            return resolution;
        }
    }
}
