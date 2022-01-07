using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using GroupBy.Application.Design.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GroupBy.Application.Exceptions;

namespace GroupBy.Data.Repositories
{
    public class GroupRepository : AsyncRepository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context) : base(context)
        {

        }

        public async Task AddMamber(int groupId, int volunteerId)
        {
            var group = await context.Set<Group>().Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                throw new NotFoundException("Group", groupId);

            var volunteer = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == volunteerId);
            if (volunteer == null)
                throw new NotFoundException("Volunteer", volunteer);

            group.Members.Add(volunteer);

            await context.SaveChangesAsync();
        }

        public override async Task<Group> CreateAsync(Group domain)
        {
            int ownerId = domain.Owner.Id;
            domain.Owner = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == ownerId);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", ownerId);

            if (domain.ParentGroup != null)
            {
                int parentGroupId = domain.ParentGroup.Id;
                domain.ParentGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == parentGroupId);
                if (domain.ParentGroup == null)
                    throw new NotFoundException("Group", parentGroupId);
            }
            var createdGroup = await context.Set<Group>().AddAsync(domain);

            await context.SaveChangesAsync();

            return createdGroup.Entity;
        }

        public override async Task<Group> GetAsync(Group domain)
        {
            Group g = await context.Set<Group>()
                .Include(g => g.ParentGroup)
                .Include(g => g.ChildGroups)
                .FirstOrDefaultAsync(g => g.Id == domain.Id);
            if (g == null)
                throw new NotFoundException("Group", domain.Id);

            return g;
        }

        public async Task<IEnumerable<Group>> GetSubgroupsAsync(int groupId)
        {
            return (await GetAsync(new Group { Id = groupId })).ChildGroups;
        }

        public async Task<IEnumerable<Volunteer>> GetVolunteersAsync(int group)
        {
            Group g = await context.Set<Group>().Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == group);
            if (g == null)
                throw new NotFoundException("Group", group);

            return g.Members.Where(v => v.Confirmed);
        }

        public async Task<bool> IsMember(int groupId, int volunteerId)
        {
            var group = await context.Set<Group>().Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
            return group.Members.Contains(await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == volunteerId));
        }

        public override async Task<Group> UpdateAsync(Group domain)
        {
            Group g = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == domain.Id);
            if (g == null)
                throw new NotFoundException("Group", domain.Id);

            int ownerId = domain.Owner.Id;
            domain.Owner = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == domain.Owner.Id);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", ownerId);

            g.Name = domain.Name;
            g.Description = domain.Description;
            g.Owner = domain.Owner;

            await context.SaveChangesAsync();
            return g;
        }
    }
}
