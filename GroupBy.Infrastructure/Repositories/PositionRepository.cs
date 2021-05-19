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
    public class PositionRepository : AsyncRepository<Position>, IPositionRepository
    {
        public PositionRepository(DbContext context) : base(context)
        {

        }
        public override async Task<Position> GetAsync(Position domain)
        {
            Position p = await context.Set<Position>().FirstOrDefaultAsync(p => p.Id == domain.Id);
            if (p == null)
                throw new NotFoundException("Position", domain.Id);
            return p;
        }

        public override async Task<Position> UpdateAsync(Position domain)
        {
            Position position = await context.Set<Position>().Include(p => p.HigherPosition).FirstOrDefaultAsync(p => p.Id == domain.Id);
            if (position == null)
                throw new NotFoundException("Position", domain.Id);

            position.Name = domain.Name;
            Position higherPosition = position.HigherPosition;
            if(domain.HigherPosition != null)
            {
                higherPosition = await context.Set<Position>().FirstOrDefaultAsync(p => p.Id == domain.HigherPosition.Id);
                if (higherPosition == null)
                    throw new NotFoundException("Position", domain.HigherPosition.Id);
            }
            position.HigherPosition = higherPosition;
            await context.SaveChangesAsync();
            return position;
        }
        public override async Task<Position> CreateAsync(Position domain)
        {
            if(domain.HigherPosition != null)
            {
                int higherId = domain.HigherPosition.Id;
                domain.HigherPosition = await context.Set<Position>().FirstOrDefaultAsync(p => p.Id == domain.HigherPosition.Id);
                if (domain.HigherPosition == null)
                    throw new NotFoundException("Position", higherId);
            }
            await base.CreateAsync(domain);
            return domain;
        }
    }
}
