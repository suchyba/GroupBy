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
        public VolunteerRepository(DbContext context) : base(context)
        {

        }

        public override async Task DeleteAsync(Volunteer domain)
        {
            Volunteer v = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == domain.Id);
            if (v == null)
                throw new NotFoundException("Volunteer", domain.Id);
            context.Set<Volunteer>().Remove(v);
            await context.SaveChangesAsync();
        }

        public override async Task<Volunteer> GetAsync(Volunteer domain)
        {
            Volunteer v = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == domain.Id);
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
    }
}
