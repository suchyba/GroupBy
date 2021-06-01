using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Position;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class PositionService : AsyncService<Position, PositionDTO, PositionCreateDTO, PositionDTO>, IPositionService
    {
        public PositionService(IPositionRepository positionRepository, IMapper mapper, 
            IValidator<PositionDTO> updateValidator, IValidator<PositionCreateDTO> createValidator) 
            : base(positionRepository, mapper, updateValidator, createValidator)
        {

        }
    }
}
