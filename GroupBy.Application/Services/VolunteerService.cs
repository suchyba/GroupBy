using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.Validators;
using GroupBy.Application.ViewModels;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class VolunteerService : AsyncService<Volunteer, VolunteerViewModel, VolunteerCreateViewModel, VolunteerUpdateViewModel>, IVolunteerService
    {
        public VolunteerService(IVolunteerRepository volunteerRepository, IMapper mapper,
            IValidator<VolunteerCreateViewModel> createValidator,
            IValidator<VolunteerUpdateViewModel> updateValidator) : base(volunteerRepository, mapper, updateValidator, createValidator)
        {

        }
        public Task<IEnumerable<GroupViewModel>> GetGroupsAsync(int volunteerId)
        {
            throw new NotImplementedException();
        }
    }
}
