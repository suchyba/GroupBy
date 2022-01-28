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
        private readonly IVolunteerRepository volunteerRepository;

        public GroupRepository(DbContext context, IVolunteerRepository volunteerRepository) : base(context)
        {
            this.volunteerRepository = volunteerRepository;
        }

        public async Task AddMemberAsync(int groupId, int volunteerId)
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

            await AddMemberAsync(createdGroup.Entity.Id, ownerId);

            return createdGroup.Entity;
        }

        public override async Task<Group> GetAsync(Group domain)
        {
            Group g = await context.Set<Group>()
                .Include(g => g.Owner)
                .Include(g => g.ParentGroup)
                .Include(g => g.Members)
                .Include(g => g.AccountingBooks)
                .Include(g => g.Elements)
                .Include(g => g.ChildGroups)
                .Include(g => g.RelatedProject)
                .Include(g => g.Resolutions)
                .Include(g => g.ProjectsRealisedInGroup)
                .Include(g => g.Permissions)
                .Include(g => g.ParentGroup)
                .Include(g => g.ChildGroups)
                .Include(g => g.InventoryBook)
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
            var group = await GetAsync(new Group { Id = groupId });

            var volunteer = await volunteerRepository.GetAsync(new Volunteer { Id = volunteerId });
            return group.Members.Contains(volunteer);
        }

        public async Task RemoveMemberAsync(int groupId, int volunteerId)
        {
            if (!await IsMember(groupId, volunteerId))
                throw new BadRequestException("Volunteer is not a member of the group");

            var group = await GetAsync(new Group { Id = groupId });
            var volunteer = await volunteerRepository.GetAsync(new Volunteer { Id = volunteerId });

            if (group.Owner == volunteer)
                throw new BadRequestException("Volunteer cannot be an owner of the group");

            group.Members.Remove(volunteer);

            await context.SaveChangesAsync();
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
