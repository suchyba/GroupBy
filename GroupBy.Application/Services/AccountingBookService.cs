using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.DTO.FinancialRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingBookService : AsyncService<AccountingBook, AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>, IAccountingBookService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IAccountingBookTemplateRepository accountingBookTemplateRepository;

        public AccountingBookService(
            IAccountingBookRepository accountingBookRepository,
            IGroupRepository groupRepository,
            IAccountingBookTemplateRepository accountingBookTemplateRepository,
            IMapper mapper,
            IValidator<AccountingBookSimpleDTO> updateValidator,
            IValidator<AccountingBookCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(accountingBookRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
            this.accountingBookTemplateRepository = accountingBookTemplateRepository;
        }

        public override async Task<AccountingBookDTO> GetAsync(AccountingBookSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<AccountingBookDTO>(await repository.GetAsync(mapper.Map<AccountingBook>(model), includes: new string[] { "Records.Values", "RelatedGroup", "Categories" }));
            }
        }

        protected override async Task<AccountingBook> CreateOperationAsync(AccountingBookCreateDTO model)
        {
            var domain = mapper.Map<AccountingBook>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                domain.RelatedGroup = await groupRepository.GetAsync(domain.RelatedGroup);

                domain.Records = new List<FinancialRecord>();

                var accountingBookTemplate = await accountingBookTemplateRepository.GetAsync(new { Id = model.AccountingBookTemplateId }, includes: "Categories");

                domain.Categories = accountingBookTemplate.Categories;

                var createdBook = await repository.CreateAsync(domain);

                await uow.Commit();
                return createdBook;
            }
        }

        public async Task<IEnumerable<FinancialRecordSimpleDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain)
        {
            using (unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<FinancialRecordSimpleDTO>>(await (repository as IAccountingBookRepository).GetFinancialRecordsAsync(mapper.Map<AccountingBook>(domain)));
            }
        }

        protected override async Task<AccountingBook> UpdateOperationAsync(AccountingBook entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                AccountingBook updatedObject = await repository.UpdateAsync(entity);
                updatedObject = await repository.GetAsync(new { Id = updatedObject.Id }, includes: "Records");
                await uow.Commit();
                return updatedObject;
            }
        }
    }
}
