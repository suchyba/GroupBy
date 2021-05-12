using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public abstract class AsyncService <Domain, ViewModel, CreateViewModel, UpdateViewModel> : IAsyncService<ViewModel, CreateViewModel, UpdateViewModel>
    {
        protected readonly IAsyncRepository<Domain> repository;
        protected readonly IMapper mapper;
        protected readonly IValidator<UpdateViewModel> updateValidator;
        protected readonly IValidator<CreateViewModel> createValidator;

        public AsyncService(IAsyncRepository<Domain> repository, IMapper mapper, IValidator<UpdateViewModel> updateValidator, IValidator<CreateViewModel> createValidator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.updateValidator = updateValidator;
            this.createValidator = createValidator;
        }
        public virtual async Task<ViewModel> CreateAsync(CreateViewModel model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<ViewModel>(await repository.CreateAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task DeleteAsync(ViewModel model)
        {
            await repository.DeleteAsync(mapper.Map<Domain>(model));
        }

        public virtual async Task<IEnumerable<ViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<ViewModel>>(await repository.GetAllAsync());
        }

        public virtual async Task<ViewModel> GetAsync(ViewModel model)
        {
            return mapper.Map<ViewModel>(await repository.GetAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task<ViewModel> UpdateAsync(UpdateViewModel model)
        {
            var validationResult = await updateValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<ViewModel>(await repository.UpdateAsync(mapper.Map<Domain>(model)));
        }
    }
}
