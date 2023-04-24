using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.InventoryItemSource;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class InventoryItemSourceService : AsyncService<InventoryItemSource, InventoryItemSourceDTO, InventoryItemSourceDTO, InventoryItemSourceCreateDTO, InventoryItemSourceDTO>, IInventoryItemSourceService
    {
        public InventoryItemSourceService(
            IInventoryItemSourceRepository repository,
            IMapper mapper,
            IValidator<InventoryItemSourceDTO> updateValidator,
            IValidator<InventoryItemSourceCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
