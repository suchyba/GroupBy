﻿using GroupBy.Application.Design.Repositories;
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
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IProjectRepository projectRepository;

        public FinancialIncomeRecordRepository(
            DbContext context,
            IAccountingDocumentRepository accountingDocumentRepository,
            IAccountingBookRepository accountingBookRepository,
            IProjectRepository projectRepository) : base(context)
        {
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.accountingBookRepository = accountingBookRepository;
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

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            await context.SaveChangesAsync();
            return record;
        }
        public override async Task<FinancialIncomeRecord> CreateAsync(FinancialIncomeRecord domain)
        {
            domain.Book = await accountingBookRepository.GetAsync(new AccountingBook { BookId = domain.BookId, BookOrderNumberId = domain.BookOrderNumberId });

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot add record to locked book");

            domain.RelatedDocument = await accountingDocumentRepository.GetAsync(domain.RelatedDocument);

            if (domain.RelatedProject != null)
                domain.RelatedProject = await projectRepository.GetAsync(domain.RelatedProject);
            else
                domain.RelatedProject = null;

            var newRecord = await context.Set<FinancialIncomeRecord>().AddAsync(domain);
            await context.SaveChangesAsync();
            return newRecord.Entity;
        }
        public async override Task DeleteAsync(FinancialIncomeRecord domain)
        {
            domain = await GetAsync(domain);

            if (domain.Book.Locked)
                throw new BadRequestException("Cannot remove record from locked book");

            await base.DeleteAsync(domain);
        }
    }
}
