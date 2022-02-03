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
    public class RankRepository : AsyncRepository<Rank>, IRankRepository
    {
        public RankRepository(DbContext context) : base(context)
        {

        }

        public override async Task<Rank> GetAsync(Rank domain)
        {
            var r = await context.Set<Rank>()
                .Include(r => r.HigherRank)
                .FirstOrDefaultAsync(r => r.Id == domain.Id);
            if (r == null)
                throw new NotFoundException("Rank", domain.Id);

            return r;
        }

        public async Task<int> GetMaxIdAsync()
        {
            try
            {
                return await context.Set<Rank>().MaxAsync(r => r.Id);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override async Task<Rank> UpdateAsync(Rank domain)
        {
            var r = await GetAsync(domain);
            Rank higherRank = null;
            if (domain.HigherRank != null)
            {
                higherRank = await context.Set<Rank>().FirstOrDefaultAsync(r => r.Id == domain.HigherRank.Id);
                if (higherRank == null)
                    throw new NotFoundException("Rank", domain.HigherRank.Id);
            }
            r.HigherRank = higherRank;
            r.Name = domain.Name;

            await context.SaveChangesAsync();

            return r;
        }
        public override async Task<Rank> CreateAsync(Rank domain)
        {
            if (domain.HigherRank != null)
            {
                int higherRankId = domain.HigherRank.Id;
                domain.HigherRank = await context.Set<Rank>().FirstOrDefaultAsync(r => r.Id == higherRankId);
                if (domain.HigherRank == null)
                    throw new NotFoundException("Rank", higherRankId);
            }
            return await base.CreateAsync(domain);
        }
    }
}
