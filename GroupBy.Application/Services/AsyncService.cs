using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public abstract class AsyncService <Domain, SimpleDTO, FullDTO, CreateDTO, UpdateDTO> : IAsyncService<SimpleDTO, FullDTO, CreateDTO, UpdateDTO>
    {
        protected readonly IAsyncRepository<Domain> repository;
        protected readonly IMapper mapper;
        protected readonly IValidator<UpdateDTO> updateValidator;
        protected readonly IValidator<CreateDTO> createValidator;

        public AsyncService(IAsyncRepository<Domain> repository, IMapper mapper, IValidator<UpdateDTO> updateValidator, IValidator<CreateDTO> createValidator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.updateValidator = updateValidator;
            this.createValidator = createValidator;
        }
        public virtual async Task<FullDTO> CreateAsync(CreateDTO model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<FullDTO>(await repository.CreateAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task DeleteAsync(SimpleDTO model)
        {
            await repository.DeleteAsync(mapper.Map<Domain>(model));
        }

        public virtual async Task<IEnumerable<SimpleDTO>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<SimpleDTO>>(await repository.GetAllAsync());
        }

        public virtual async Task<FullDTO> GetAsync(SimpleDTO model)
        {
            return mapper.Map<FullDTO>(await repository.GetAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task<FullDTO> UpdateAsync(UpdateDTO model)
        {
            var validationResult = await updateValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<FullDTO>(await repository.UpdateAsync(mapper.Map<Domain>(model)));
        }
    }
}
