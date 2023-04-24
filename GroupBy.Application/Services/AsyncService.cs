using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public abstract class AsyncService<Domain, SimpleDTO, FullDTO, CreateDTO, UpdateDTO> : IAsyncService<SimpleDTO, FullDTO, CreateDTO, UpdateDTO>
        where Domain : class
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
        public async Task<FullDTO> CreateAsync(CreateDTO model)
        {
            await ValidateCreateAsync(model);
            return mapper.Map<FullDTO>(await CreateOperationAsync(mapper.Map<Domain>(model)));
        }

        protected virtual async Task ValidateCreateAsync(CreateDTO model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);
        }

        protected virtual async Task<Domain> CreateOperationAsync(Domain entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                Domain createdObject = await repository.CreateAsync(mapper.Map<Domain>(entity));
                await uow.Commit();

                return createdObject;
            }
        }

        public virtual async Task DeleteAsync(SimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var entity = await repository.GetAsync(mapper.Map<Domain>(model));
                await repository.DeleteAsync(entity);

                try
                {
                    await uow.Commit();
                }
                catch (DbUpdateException)
                {
                    throw new DeleteNotPermittedException(typeof(Domain).Name);
                }
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

        public async Task<FullDTO> UpdateAsync(UpdateDTO model)
        {
            await ValidateUpdateAsync(model);
            var domain = mapper.Map<Domain>(model);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                // Checking if object exists in db
                await repository.GetAsync(domain, asTracking: false);

                var updatedEntity = await UpdateOperationAsync(domain);

                await uow.Commit();
                return mapper.Map<FullDTO>(updatedEntity);
            }
        }

        protected virtual async Task ValidateUpdateAsync(UpdateDTO model)
        {
            var validationResult = await updateValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);
        }

        protected virtual async Task<Domain> UpdateOperationAsync(Domain entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                Domain updatedObject = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedObject;
            }
        }
    }
}
