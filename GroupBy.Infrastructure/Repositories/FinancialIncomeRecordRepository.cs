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
    public class FinancialIncomeRecordRepository : AsyncRepository<FinancialIncomeRecord>, IFinancialIncomeRecordRepository
    {
        private readonly IAccountingDocumentRepository accountingDocumentRepository;
        private readonly IProjectRepository projectRepository;

        public FinancialIncomeRecordRepository(
            DbContext context,
            IAccountingDocumentRepository accountingDocumentRepository,
            IProjectRepository projectRepository) : base(context)
        {
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<FinancialIncomeRecord> GetAsync(FinancialIncomeRecord domain)
        {
            var record = await context.Set<FinancialIncomeRecord>()
                .Include(r => r.RelatedProject)
                .Include(r => r.RelatedDocument)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == domain.Id);
            if (record == null)
                throw new NotFoundException("FinancialIncomeRecord", domain.Id);
            return record;
        }

        public override async Task<FinancialIncomeRecord> UpdateAsync(FinancialIncomeRecord domain)
        {
            var record = await GetAsync(domain);
            
            record.Date = domain.Date;
            record.Description = domain.Description;
            record.MembershipFee = domain.MembershipFee;
            record.ProgramFee = domain.ProgramFee;
            record.OnePercent = domain.OnePercent;
            record.Other = domain.Other;
            record.EarningAction = domain.EarningAction;
            record.Dotation = domain.Dotation;

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            await context.SaveChangesAsync();
            return record;
        }
        public override async Task<FinancialIncomeRecord> CreateAsync(FinancialIncomeRecord domain)
        {
            domain.RelatedDocument = await accountingDocumentRepository.GetAsync(domain.RelatedDocument);

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            var newRecord = await context.Set<FinancialIncomeRecord>().AddAsync(domain);
            await context.SaveChangesAsync();
            return newRecord.Entity;
        }
    }
}
