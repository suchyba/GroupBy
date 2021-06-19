using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public abstract class AsyncService <Domain, DTO, CreateDTO, UpdateDTO> : IAsyncService<DTO, CreateDTO, UpdateDTO>
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
        public virtual async Task<DTO> CreateAsync(CreateDTO model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<DTO>(await repository.CreateAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task DeleteAsync(DTO model)
        {
            await repository.DeleteAsync(mapper.Map<Domain>(model));
        }

        public virtual async Task<IEnumerable<DTO>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<DTO>>(await repository.GetAllAsync());
        }

        public virtual async Task<DTO> GetAsync(DTO model)
        {
            return mapper.Map<DTO>(await repository.GetAsync(mapper.Map<Domain>(model)));
        }

        public virtual async Task<DTO> UpdateAsync(UpdateDTO model)
        {
            var validationResult = await updateValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<DTO>(await repository.UpdateAsync(mapper.Map<Domain>(model)));
        }
    }
}
