using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class PositionRepository : AsyncRepository<Position>, IPositionRepository
    {
        public PositionRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public override async Task<Position> UpdateAsync(Position domain)
        {
            Position position = await GetAsync(domain, false, "HigherPosition");
            if (position == null)
                throw new NotFoundException("Position", domain.Id);

            position.Name = domain.Name;
            Position higherPosition = position.HigherPosition;
            if (domain.HigherPosition != null)
            {
                higherPosition = await GetAsync(domain.HigherPosition);
                if (higherPosition == null)
                    throw new NotFoundException("Position", domain.HigherPosition.Id);
            }
            position.HigherPosition = higherPosition;
            return position;
        }
        public override async Task<Position> CreateAsync(Position domain)
        {
            if (domain.HigherPosition != null)
            {
                Guid higherId = domain.HigherPosition.Id;
                domain.HigherPosition = await GetAsync(domain.HigherPosition);
                if (domain.HigherPosition == null)
                    throw new NotFoundException("Position", higherId);
            }
            await base.CreateAsync(domain);
            return domain;
        }
    }
}
