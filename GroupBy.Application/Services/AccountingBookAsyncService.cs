using AutoMapper;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.Validators;
using GroupBy.Application.ViewModels;
using GroupBy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingBookAsyncService : IAccountingBookAsyncService
    {
        private readonly IAccountingBookAsyncRepository accountingBookRepository;
        private readonly IMapper mapper;

        public AccountingBookAsyncService(IAccountingBookAsyncRepository accountingBookRepository, IMapper mapper)
        {
            this.accountingBookRepository = accountingBookRepository;
            this.mapper = mapper;
        }
        public async Task<AccountingBookViewModel> CreateAsync(AccountingBookViewModel model)
        {
            AccountingBookValidator validator = new(accountingBookRepository);
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            return mapper.Map<AccountingBookViewModel>(await accountingBookRepository.CreateAsync(mapper.Map<AccountingBook>(model)));
        }

        public async Task DeleteAsync(Guid id)
        {
            await accountingBookRepository.DeleteAsync(id);
        }

        public async Task<AccountingBookViewModel> GetAsync(Guid id)
        {
            return mapper.Map<AccountingBookViewModel>(await accountingBookRepository.GetAsync(id));
        }

        public async Task<IEnumerable<AccountingBookViewModel>> GetAllAsync()
        {
            return mapper.Map<List<AccountingBookViewModel>>(await accountingBookRepository.GetAllAsync());
        }

        public async Task<AccountingBookViewModel> UpdateAsync(AccountingBookViewModel model)
        {
            AccountingBookValidator validator = new(accountingBookRepository);
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            return mapper.Map<AccountingBookViewModel>(await accountingBookRepository.UpdateAsync(mapper.Map<AccountingBook>(model)));
        }
    }
}
