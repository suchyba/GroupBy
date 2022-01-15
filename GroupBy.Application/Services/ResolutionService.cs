using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Resolution;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class ResolutionService : AsyncService<Resolution, ResolutionDTO, ResolutionCreateDTO, ResolutionUpdateDTO>, IResolutionService
    {
        public ResolutionService(
            IResolutionRepository repository,
            IMapper mapper,
            IValidator<ResolutionUpdateDTO> updateValidator,
            IValidator<ResolutionCreateDTO> createValidator)
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
