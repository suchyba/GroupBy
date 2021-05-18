using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels.Rank;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class RankService : AsyncService<Rank, RankViewModel, RankCreateViewModel, RankViewModel>, IRankService
    {
        public RankService(IRankRepository repository, IMapper mapper, 
            IValidator<RankViewModel> validator, IValidator<RankCreateViewModel> createValidator) : base(repository, mapper, validator, createValidator)
        {

        }
    }
}
