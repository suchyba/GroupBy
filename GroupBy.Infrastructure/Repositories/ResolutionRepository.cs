using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ResolutionRepository : AsyncRepository<Resolution>, IResolutionRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IVolunteerRepository volunteerRepository;

        public ResolutionRepository(DbContext context,
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository) : base(context)
        {
            this.groupRepository = groupRepository;
            this.volunteerRepository = volunteerRepository;
        }
        public async override Task<Resolution> CreateAsync(Resolution domain)
        {
            domain.Group = await groupRepository.GetAsync(domain.Group);
            domain.Legislator = await volunteerRepository.GetAsync(domain.Legislator);

            if (!domain.Group.Members.Contains(domain.Legislator))
                throw new BadRequestException("Legistlator must be a member of the group");

            var resolution = await context.Set<Resolution>().AddAsync(domain);
            await context.SaveChangesAsync();
            return resolution.Entity;
        }

        public async override Task<Resolution> GetAsync(Resolution domain)
        {
            Resolution resolution = await context.Set<Resolution>()
                .Include(r => r.Legislator)
                .Include(r => r.Group)
                .FirstOrDefaultAsync(r => r.Id == domain.Id);
            if (resolution == null)
                throw new NotFoundException("Resolution", domain.Id);
            return resolution;
        }

        public override async Task<IEnumerable<Resolution>> GetAllAsync()
        {
            return await context.Set<Resolution>()
                .Include(r => r.Group)
                .Include(r => r.Legislator)
                .ToListAsync();
        }

        public async override Task<Resolution> UpdateAsync(Resolution domain)
        {
            Resolution resolution = await GetAsync(domain);
            resolution.Content = domain.Content;
            resolution.Date = domain.Date;
            resolution.Symbol = domain.Symbol;

            await context.SaveChangesAsync();
            return resolution;
        }
    }
}
