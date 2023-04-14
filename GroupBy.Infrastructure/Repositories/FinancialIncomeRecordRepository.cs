using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Design.Exceptions;
using GroupBy.Data.DbContexts;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class FinancialIncomeRecordRepository : AsyncRepository<FinancialIncomeRecord>, IFinancialIncomeRecordRepository
    {
        private readonly IAccountingDocumentRepository accountingDocumentRepository;
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IProjectRepository projectRepository;

        public FinancialIncomeRecordRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IAccountingDocumentRepository accountingDocumentRepository,
            IAccountingBookRepository accountingBookRepository,
            IProjectRepository projectRepository) : base(dBcontextLocator)
        {
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.accountingBookRepository = accountingBookRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<FinancialIncomeRecord> UpdateAsync(FinancialIncomeRecord domain)
        {
            var record = await GetAsync(domain, false, "Book");

            if (record.Book.Locked)
                throw new BadRequestException("Cannot update record in locked book");

            record.Date = domain.Date;
            record.Description = domain.Description;
            record.MembershipFee = domain.MembershipFee;
            record.ProgramFee = domain.ProgramFee;
            record.OnePercent = domain.OnePercent;
            record.Other = domain.Other;
            record.EarningAction = domain.EarningAction;
            record.Dotation = domain.Dotation;

            return record;
        }
        public override async Task<FinancialIncomeRecord> CreateAsync(FinancialIncomeRecord domain)
        {
            domain.Book = await accountingBookRepository.GetAsync(domain.Book, false, "RelatedGroup");

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot add record to locked book");

            domain.RelatedDocument = await accountingDocumentRepository.GetAsync(domain.RelatedDocument, false, "Groups");
            if (!domain.RelatedDocument.Groups.Contains(domain.Book.RelatedGroup))
                throw new BadRequestException("Document and accounting book must be related with the same group");

            if (domain.RelatedProject != null)
            {
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject, false, "ParentGroup");
                if (domain.RelatedProject.ParentGroup != domain.Book.RelatedGroup
                    && domain.RelatedProject.ProjectGroup != domain.Book.RelatedGroup)
                    throw new BadRequestException("Project and accounting book must be related with the same group");
            }

            var newRecord = await context.Set<FinancialIncomeRecord>().AddAsync(domain);
            return newRecord.Entity;
        }
        public async override Task DeleteAsync(FinancialIncomeRecord domain)
        {
            domain = await GetAsync(domain, false, "Book");

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot remove record from locked book");

            await base.DeleteAsync(domain);
        }
    }
}
