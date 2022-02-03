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
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IProjectRepository projectRepository;

        public FinancialOutcomeRecordRepository(
            DbContext context,
            IAccountingDocumentRepository accountingDocumentRepository,
            IAccountingBookRepository accountingBookRepository,
            IProjectRepository projectRepository) : base(context)
        {
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.accountingBookRepository = accountingBookRepository;
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
                throw new NotFoundException("FinancialOutcomeRecord", domain.Id);
            return record;
        }

        public override async Task<FinancialOutcomeRecord> UpdateAsync(FinancialOutcomeRecord domain)
        {
            var record = await GetAsync(domain);

            if (record.Book.Locked)
                throw new BadRequestException("Cannot update record in locked book");

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

            await context.SaveChangesAsync();
            return record;
        }
        public override async Task<FinancialOutcomeRecord> CreateAsync(FinancialOutcomeRecord domain)
        {
            domain.Book = await accountingBookRepository.GetAsync(new AccountingBook { BookId = domain.BookId, BookOrderNumberId = domain.BookOrderNumberId });

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot add record to locked book");

            domain.RelatedDocument = await accountingDocumentRepository.GetAsync(domain.RelatedDocument);
            if (domain.RelatedDocument.Group != domain.Book.RelatedGroup)
                throw new BadRequestException("Document and accounting book must be related with the same group");

            if (domain.RelatedProject != null)
            {
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
                if (domain.RelatedProject.ParentGroup != domain.Book.RelatedGroup)
                    throw new BadRequestException("Project and accounting book must be related with the same group");
            }

            var newRecord = await context.Set<FinancialOutcomeRecord>().AddAsync(domain);
            await context.SaveChangesAsync();
            return newRecord.Entity;
        }
        public async override Task DeleteAsync(FinancialOutcomeRecord domain)
        {
            domain = await GetAsync(domain);

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot remove record from locked book");

            await base.DeleteAsync(domain);
        }
    }
}
