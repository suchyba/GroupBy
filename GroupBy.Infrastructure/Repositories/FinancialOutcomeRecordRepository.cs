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
    public class FinancialOutcomeRecordRepository : AsyncRepository<FinancialOutcomeRecord>, IFinancialOutcomeRecordRepository
    {
        private readonly IAccountingDocumentRepository accountingDocumentRepository;
        private readonly IProjectRepository projectRepository;

        public FinancialOutcomeRecordRepository(
            DbContext context,
            IAccountingDocumentRepository accountingDocumentRepository,
            IProjectRepository projectRepository) : base(context)
        {
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<FinancialOutcomeRecord> GetAsync(FinancialOutcomeRecord domain)
        {
            var record = await context.Set<FinancialOutcomeRecord>()
                .Include(r => r.RelatedProject)
                .Include(r => r.RelatedDocument)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == domain.Id);
            if (record == null)
                throw new NotFoundException("FinancialOutcomeRecord", domain.Id );
            return record;
        }

        public override async Task<FinancialOutcomeRecord> UpdateAsync(FinancialOutcomeRecord domain)
        {
            var record = await GetAsync(domain);

            record.Accommodation = domain.Accommodation;
            record.Date = domain.Date;
            record.Description = domain.Description;
            record.Food = domain.Food;
            record.Insurance = domain.Insurance;
            record.Inventory = domain.Inventory;
            record.Material = domain.Material;
            record.Other = domain.Other;
            record.Salary = domain.Salary;
            record.Service = domain.Service;
            record.Transport = domain.Transport;

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            await context.SaveChangesAsync();
            return record;
        }
        public override async Task<FinancialOutcomeRecord> CreateAsync(FinancialOutcomeRecord domain)
        {
            domain.RelatedDocument = await accountingDocumentRepository.GetAsync(domain.RelatedDocument);

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            var newRecord = await context.Set<FinancialOutcomeRecord>().AddAsync(domain);
            await context.SaveChangesAsync();
            return newRecord.Entity;
        }
    }
}
