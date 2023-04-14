using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Design.Exceptions;
using GroupBy.Data.DbContexts;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class AccountingDocumentRepository : AsyncRepository<AccountingDocument>, IAccountingDocumentRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IProjectRepository projectRepository;

        public AccountingDocumentRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IGroupRepository groupRepository,
            IProjectRepository projectRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<AccountingDocument> UpdateAsync(AccountingDocument domain)
        {
            var document = await GetAsync(domain);
            document.Name = domain.Name;
            document.FilePath = domain.FilePath;

            return document;
        }
        public override async Task<AccountingDocument> CreateAsync(AccountingDocument domain)
        {
            var tempGroups = new List<Group>();
            foreach (var group in domain.Groups)
            {
                tempGroups.Add(await groupRepository.GetAsync(group));
            }

            domain.Groups = tempGroups;
            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);

            await context.Set<AccountingDocument>().AddAsync(domain);

            return domain;
        }
    }
}
