using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookService : AsyncService<InventoryBook, InventoryBookDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>, IInventoryBookService
    {
        public InventoryBookService(IInventoryBookRepository repository, IMapper mapper, 
            IValidator<InventoryBookUpdateDTO> updateValidator, IValidator<InventoryBookCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
