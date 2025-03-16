using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.AccountingBookTemplate;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingBookTemplateService : AsyncService<AccountingBookTemplate, AccountingBookTemplateSimpleDTO, AccountingBookTemplateDTO, AccountingBookTemplateCreateDTO, AccountingBookTemplateUpdateDTO>, IAccountingBookTemplateService
    {
        private readonly IFinancialCategoryRepository financialCategoryRepository;

        public AccountingBookTemplateService(
            IAccountingBookTemplateRepository repository,
            IFinancialCategoryRepository financialCategoryRepository,
            IMapper mapper,
            IValidator<AccountingBookTemplateUpdateDTO> updateValidator,
            IValidator<AccountingBookTemplateCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory) : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.financialCategoryRepository = financialCategoryRepository;
        }

        public async override Task<AccountingBookTemplateDTO> GetAsync(AccountingBookTemplateSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<AccountingBookTemplateDTO>(await repository.GetAsync(mapper.Map<AccountingBookTemplate>(model), includes: "Categories"));
            }
        }

        protected async override Task<AccountingBookTemplate> UpdateOperationAsync(AccountingBookTemplate entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var bookToUpdate = await repository.GetAsync(entity, includes: "Categories");

                List<FinancialCategory> categories = new();

                foreach (var category in entity.Categories)
                {
                    categories.Add(await financialCategoryRepository.GetAsync(category));
                }

                bookToUpdate.Categories = categories;
                bookToUpdate.Name = entity.Name;
                bookToUpdate.Description = entity.Description;

                AccountingBookTemplate updatedObject = await repository.UpdateAsync(bookToUpdate);

                await uow.Commit();

                return updatedObject;
            }
        }

        protected override async Task<AccountingBookTemplate> CreateOperationAsync(AccountingBookTemplateCreateDTO model)
        {
            var bookToCreate = mapper.Map<AccountingBookTemplate>(model);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                List<FinancialCategory> categories = new();

                foreach (var category in bookToCreate.Categories)
                {
                    categories.Add(await financialCategoryRepository.GetAsync(category));
                }

                bookToCreate.Categories = categories;

                var createdBook = await repository.CreateAsync(bookToCreate);

                await uow.Commit();

                return createdBook;
            }
        }
    }
}
