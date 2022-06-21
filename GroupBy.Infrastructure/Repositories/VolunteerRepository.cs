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
    public class VolunteerRepository : AsyncRepository<Volunteer>, IVolunteerRepository
    {
        private readonly IRankRepository rankRepository;

        public VolunteerRepository(DbContext context, IRankRepository rankRepository) : base(context)
        {
            this.rankRepository = rankRepository;
        }
        public override async Task<IEnumerable<Volunteer>> GetAllAsync()
        {
            return await context.Set<Volunteer>().Include(v => v.Identity).ToListAsync();
        }
        public override async Task<Volunteer> GetAsync(Volunteer domain)
        {
            Volunteer v = await context.Set<Volunteer>()
                .Include(v => v.Rank)
                .FirstOrDefaultAsync(v => v.Id == domain.Id);
            if (v == null)
                throw new NotFoundException("Volunteer", domain.Id);
            return v;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(int volunteerId)
        {
            Volunteer v = await context.Set<Volunteer>().Include(v => v.Groups).FirstOrDefaultAsync(v => v.Id == volunteerId);
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.Groups;
        }

        public async override Task<Volunteer> CreateAsync(Volunteer domain)
        {
            if (domain.Rank != null)
                domain.Rank = await rankRepository.GetAsync(domain.Rank);

            var volunteer = await context.Set<Volunteer>().AddAsync(domain);
            await context.SaveChangesAsync();
            return volunteer.Entity;
        }

        public override async Task<Volunteer> UpdateAsync(Volunteer domain)
        {
            Volunteer v = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == domain.Id);
            if (v == null)
                throw new NotFoundException("Volunteer", domain.Id);

            v.FirstNames = domain.FirstNames;
            v.LastName = domain.LastName;
            v.PhoneNumber = domain.PhoneNumber;
            v.Address = domain.Address;
            if (domain.Rank != null)
            {
                Rank r = await context.Set<Rank>().FirstOrDefaultAsync(r => r.Id == domain.Rank.Id);
                if (r == null)
                    throw new NotFoundException("Rank", domain.Rank.Id);
                v.Rank = r;
            }
            else
                v.Rank = null;

            await context.SaveChangesAsync();

            return v;
        }

        public async Task<IEnumerable<Group>> GetOwnedGroupsAsync(int volunteerId)
        {
            Volunteer v = await context.Set<Volunteer>().Include(v => v.OwnedGroups).FirstOrDefaultAsync(v => v.Id == volunteerId);
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedGroups;
        }

        public async Task<IEnumerable<Project>> GetOwnedProjectsAsync(int volunteerId)
        {
            Volunteer v = await context.Set<Volunteer>().Include(v => v.OwnedProjects).FirstOrDefaultAsync(v => v.Id == volunteerId);
            if (v == null)
                throw new NotFoundException("Volunteer", volunteerId);

            return v.OwnedProjects;
        }
    }
}
