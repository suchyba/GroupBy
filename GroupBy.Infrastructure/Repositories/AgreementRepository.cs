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
    public class AgreementRepository : AsyncRepository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(DbContext context) : base(context)
        {

        }
        public override async Task<Agreement> GetAsync(Agreement domain)
        {
            var a = await context.Set<Agreement>().FirstOrDefaultAsync(a => a.Id == domain.Id);
            if (a == null)
                throw new NotFoundException("Agreement", domain.Id);

            return a;
        }

        public override async Task<Agreement> UpdateAsync(Agreement domain)
        {
            var toModify = await GetAsync(domain);

            toModify.Content = domain.Content;

            await context.SaveChangesAsync();

            return toModify;
        }
    }
}
