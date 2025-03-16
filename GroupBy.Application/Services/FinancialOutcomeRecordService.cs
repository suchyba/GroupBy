﻿using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.FinancialOutcomeRecord;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class FinancialOutcomeRecordService : AsyncService<FinancialOutcomeRecord, FinancialOutcomeRecordSimpleDTO, FinancialOutcomeRecordDTO, FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecordUpdateDTO>, IFinancialOutcomeRecordService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IAccountingDocumentRepository accountingDocumentRepository;
        private readonly IFinancialCategoryRepository financialCategoryRepository;
        private readonly IFinancialCategoryValueRepository financialCategoryValueRepository;

        public FinancialOutcomeRecordService(
            IFinancialOutcomeRecordRepository repository,
            IProjectRepository projectRepository,
            IAccountingBookRepository accountingBookRepository,
            IAccountingDocumentRepository accountingDocumentRepository,
            IFinancialCategoryRepository financialCategoryRepository,
            IFinancialCategoryValueRepository financialCategoryValueRepository,
            IMapper mapper,
            IValidator<FinancialOutcomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialOutcomeRecordCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.projectRepository = projectRepository;
            this.accountingBookRepository = accountingBookRepository;
            this.accountingDocumentRepository = accountingDocumentRepository;
            this.financialCategoryRepository = financialCategoryRepository;
            this.financialCategoryValueRepository = financialCategoryValueRepository;
        }

        public override async Task<FinancialOutcomeRecordDTO> GetAsync(FinancialOutcomeRecordSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<FinancialOutcomeRecordDTO>(
                    await repository.GetAsync(mapper.Map<FinancialOutcomeRecord>(model),
                    includes: new string[] { "RelatedProject", "RelatedDocument", "Book", "Values.Category" }));
            }
        }

        public override async Task DeleteAsync(FinancialOutcomeRecordSimpleDTO model)
        {
            var entity = mapper.Map<FinancialOutcomeRecord>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity = await repository.GetAsync(entity, includes: new string[] { "Book", "Values" });

                if (entity.Book.Locked)
                    throw new BadRequestException("Cannot remove record from locked book");

                foreach (var categoryValue in entity.Values)
                {
                    await financialCategoryValueRepository.DeleteAsync(categoryValue);
                }

                await base.DeleteAsync(model);

                await uow.Commit();
            }
        }

        protected override async Task<FinancialOutcomeRecord> CreateOperationAsync(FinancialOutcomeRecordCreateDTO model)
        {
            var entity = mapper.Map<FinancialOutcomeRecord>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Book = await accountingBookRepository.GetAsync(entity.Book, false, includes: new string[] { "RelatedGroup", "Categories" });

                if (entity.Book.Locked)
                    throw new BadRequestException("Cannot add record to locked book");

                entity.RelatedDocument = await accountingDocumentRepository.GetAsync(entity.RelatedDocument, false, includes: "Groups");
                if (!entity.RelatedDocument.Groups.Contains(entity.Book.RelatedGroup))
                    throw new BadRequestException("Document and accounting book must be related with the same group");

                if (entity.RelatedProject != null)
                {
                    entity.RelatedProject = await projectRepository.GetAsync(entity.RelatedProject, false, includes: new string[] { "ParentGroup", "ProjectGroup" });
                    if (entity.RelatedProject.ParentGroup != entity.Book.RelatedGroup
                        && entity.RelatedProject.ProjectGroup != entity.Book.RelatedGroup)
                        throw new BadRequestException("Project and accounting book must be related with the same group");
                }

                foreach (var categoryValue in entity.Values)
                {
                    categoryValue.Category = await financialCategoryRepository.GetAsync(categoryValue.Category);

                    if (!entity.Book.Categories.Contains(categoryValue.Category))
                    {
                        throw new BadRequestException("Category doesn't belong to accounting book");
                    }
                }

                var createdRecord = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdRecord;
            }
        }

        protected override async Task<FinancialOutcomeRecord> UpdateOperationAsync(FinancialOutcomeRecord entity)
        {

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var recordToUpdate = await repository.GetAsync(entity, includes: new string[] { "Book.Categories", "Values.Category" });

                if (recordToUpdate.Book.Locked)
                    throw new BadRequestException("Cannot update record in locked book");

                recordToUpdate.Date = entity.Date;
                recordToUpdate.Description = entity.Description;

                foreach (var categoryValue in recordToUpdate.Values)
                {
                    var updatedValue = entity.Values.FirstOrDefault(v => v.Category.Id == categoryValue.Category.Id);

                    if (updatedValue == null)
                    {
                        await financialCategoryValueRepository.DeleteAsync(categoryValue);
                    }
                    else
                    {
                        categoryValue.Value = updatedValue.Value;
                    }
                }

                foreach (var newCategoryValue in entity.Values.Where(v => v.Id == Guid.Empty))
                {
                    newCategoryValue.Category = await financialCategoryRepository.GetAsync(newCategoryValue.Category);

                    if (!recordToUpdate.Book.Categories.Contains(newCategoryValue.Category))
                    {
                        throw new BadRequestException("Category doesn't belong to accounting book");
                    }

                    if (recordToUpdate.Values.Any(v => v.Category.Id == newCategoryValue.Category.Id))
                    {
                        throw new BadRequestException("Category already exists in record");
                    }

                    var valueList = recordToUpdate.Values.ToList();

                    valueList.Add(newCategoryValue);

                    recordToUpdate.Values = valueList;
                }

                var updatedRecord = await repository.UpdateAsync(recordToUpdate);

                await uow.Commit();

                return updatedRecord;
            }
        }
    }
}
