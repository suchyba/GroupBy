using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RankRepository : AsyncRepository<Rank>, IRankRepository
    {
        public RankRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public override async Task<Rank> UpdateAsync(Rank domain)
        {
            var r = await GetAsync(domain, false, "HigherRank");
            Rank higherRank = null;
            if (domain.HigherRank != null)
            {
                higherRank = await GetAsync(domain.HigherRank);
                if (higherRank == null)
                    throw new NotFoundException("Rank", domain.HigherRank.Id);
            }
            r.HigherRank = higherRank;
            r.Name = domain.Name;

            return r;
        }
        public override async Task<Rank> CreateAsync(Rank domain)
        {
            if (domain.HigherRank != null)
            {
                Guid higherRankId = domain.HigherRank.Id;
                domain.HigherRank = await GetAsync(domain.HigherRank);
                if (domain.HigherRank == null)
                    throw new NotFoundException("Rank", higherRankId);
            }
            return await base.CreateAsync(domain);
        }
    }
}
