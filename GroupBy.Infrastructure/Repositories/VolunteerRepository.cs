using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class VolunteerRepository : AsyncRepository<Volunteer>, IVolunteerRepository
    {
        private readonly IRankRepository rankRepository;

        public VolunteerRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator, IRankRepository rankRepository) : base(dBcontextLocator)
        {
            this.rankRepository = rankRepository;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, "Groups");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.Groups;
        }

        public async override Task<Volunteer> CreateAsync(Volunteer domain)
        {
            if (domain.Rank != null)
                domain.Rank = await rankRepository.GetAsync(domain.Rank);

            var volunteer = await context.Set<Volunteer>().AddAsync(domain);
            return volunteer.Entity;
        }

        public override async Task<Volunteer> UpdateAsync(Volunteer domain)
        {
            Volunteer v = await GetAsync(new { Id = domain.Id });
            if (v == null)
                throw new NotFoundException("Volunteer", domain.Id);

            v.FirstNames = domain.FirstNames;
            v.LastName = domain.LastName;
            v.PhoneNumber = domain.PhoneNumber;
            v.Address = domain.Address;
            if (domain.Rank != null)
            {
                Rank r = await rankRepository.GetAsync(domain.Rank);
                if (r == null)
                    throw new NotFoundException("Rank", domain.Rank.Id);
                v.Rank = r;
            }
            else
                v.Rank = null;

            return v;
        }

        public async Task<IEnumerable<Group>> GetOwnedGroupsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, "OwnedGroups");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedGroups;
        }

        public async Task<IEnumerable<Project>> GetOwnedProjectsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, "OwnedProjects");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedProjects;
        }

        public async Task<IEnumerable<RegistrationCode>> GetOwnedRegistrationCodesAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, "RegistrationCodes.TargetGroup", "RegistrationCodes.TargetRank");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.RegistrationCodes;
        }
    }
}
