using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookRecordService : AsyncService<InventoryBookRecord, InventoryBookRecordDTO, InventoryBookRecordCreateDTO, InventoryBookRecordUpdateDTO>, IInventoryBookRecordService
    {
        public InventoryBookRecordService(IInventoryBookRecordRepository repository, IMapper mapper, 
            IValidator<InventoryBookRecordUpdateDTO> updateValidator, IValidator<InventoryBookRecordCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }

    }
}
