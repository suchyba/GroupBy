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
    public class AccountingDocumentRepository : AsyncRepository<AccountingDocument>, IAccountingDocumentRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IProjectRepository projectRepository;

        public AccountingDocumentRepository(DbContext context, IGroupRepository groupRepository, IProjectRepository projectRepository) : base(context)
        {
            this.groupRepository = groupRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<AccountingDocument> GetAsync(AccountingDocument domain)
        {
            AccountingDocument document = await context.Set<AccountingDocument>().FirstOrDefaultAsync(d => d.Id == domain.Id);
            if (document == null)
                throw new NotFoundException("AccountingDocument", domain.Id);

            return document;
        }

        public override async Task<AccountingDocument> UpdateAsync(AccountingDocument domain)
        {
            var document = await GetAsync(domain);
            document.Name = domain.Name;
            document.FilePath = domain.FilePath;

            await context.SaveChangesAsync();
            return document;
        }
        public override async Task<AccountingDocument> CreateAsync(AccountingDocument domain)
        {
            domain.Group = await groupRepository.GetAsync(domain.Group);
            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);

            await context.Set<AccountingDocument>().AddAsync(domain);
            await context.SaveChangesAsync();

            return domain;
        }
    }
}
