using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class VolunteerRepository : AsyncRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, includes: "Groups");
            return v.Groups;
        }

        public async Task<IEnumerable<Group>> GetOwnedGroupsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, includes: "OwnedGroups");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedGroups;
        }

        public async Task<IEnumerable<Project>> GetOwnedProjectsAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, includes: "OwnedProjects");
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedProjects;
        }

        public async Task<IEnumerable<RegistrationCode>> GetOwnedRegistrationCodesAsync(Guid volunteerId, bool includeLocal = false)
        {
            Volunteer v = await GetAsync(new { Id = volunteerId }, includeLocal, includes: new string[] { "RegistrationCodes.TargetGroup", "RegistrationCodes.TargetRank" });
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.RegistrationCodes;
        }

        protected override Expression<Func<Volunteer, bool>> CompareKeys(object entity)
        {
            return v => entity.GetType().GetProperty("Id").GetValue(entity).Equals(v.Id);
        }
    }
}
