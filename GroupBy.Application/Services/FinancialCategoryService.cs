using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.FinancialCategory;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class FinancialCategoryService : AsyncService<FinancialCategory, FinancialCategoryDTO, FinancialCategoryDTO, FinancialCategoryCreateDTO, FinancialCategoryCreateDTO>, IFinancialCategoryService
    {
        public FinancialCategoryService(
            IFinancialCategoryRepository repository,
            IMapper mapper,
            IValidator<FinancialCategoryCreateDTO> updateValidator,
            IValidator<FinancialCategoryCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        protected override Task ValidateUpdateAsync(FinancialCategoryCreateDTO model)
        {
            throw new BadRequestException("Financial category cannot be updated.");
        }

        public async override Task DeleteAsync(FinancialCategoryDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var entity = await repository.GetAsync(mapper.Map<FinancialCategory>(model), includes: new string[] { "AccountingBooks", "AccountingBookTemplates" });

                if (entity.AccountingBooks.Count() > 0 || entity.AccountingBookTemplates.Count() > 0)
                    throw new DeleteNotPermittedException(typeof(FinancialCategory).Name);

                await repository.DeleteAsync(entity);

                try
                {
                    await uow.Commit();
                }
                catch (DbUpdateException)
                {
                    throw new DeleteNotPermittedException(typeof(FinancialCategory).Name);
                }
            }
        }
    }
}
