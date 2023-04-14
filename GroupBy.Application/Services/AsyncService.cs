using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public abstract class AsyncService<Domain, SimpleDTO, FullDTO, CreateDTO, UpdateDTO> : IAsyncService<SimpleDTO, FullDTO, CreateDTO, UpdateDTO>
    {
        protected readonly IAsyncRepository<Domain> repository;
        protected readonly IMapper mapper;
        protected readonly IValidator<UpdateDTO> updateValidator;
        protected readonly IValidator<CreateDTO> createValidator;
        protected readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public AsyncService(
            IAsyncRepository<Domain> repository,
            IMapper mapper,
            IValidator<UpdateDTO> updateValidator,
            IValidator<CreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.updateValidator = updateValidator;
            this.createValidator = createValidator;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
        public virtual async Task<FullDTO> CreateAsync(CreateDTO model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                Domain createdObject = await repository.CreateAsync(mapper.Map<Domain>(model));
                await uow.Commit();

                return mapper.Map<FullDTO>(createdObject);
            }
        }

        public virtual async Task DeleteAsync(SimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await repository.DeleteAsync(mapper.Map<Domain>(model));
                await uow.Commit();
            }
        }

        public virtual async Task<IEnumerable<SimpleDTO>> GetAllAsync(bool includeLocal = false)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<SimpleDTO>>(await repository.GetAllAsync());
            }
        }

        public virtual async Task<FullDTO> GetAsync(SimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<FullDTO>(await repository.GetAsync(mapper.Map<Domain>(model)));
            }
        }

        public virtual async Task<FullDTO> UpdateAsync(UpdateDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var validationResult = await updateValidator.ValidateAsync(model);
                if (!validationResult.IsValid)
                    throw new Design.Exceptions.ValidationException(validationResult);

                Domain updatedObject = await repository.UpdateAsync(mapper.Map<Domain>(model));
                await uow.Commit();
                return mapper.Map<FullDTO>(updatedObject);
            }
        }
    }
}
