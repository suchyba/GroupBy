using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.RegistrationCode;
using GroupBy.Design.TO.Volunteer;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class VolunteerService : AsyncService<Volunteer, VolunteerSimpleDTO, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>, IVolunteerService
    {
        public VolunteerService(
            IVolunteerRepository volunteerRepository,
            IMapper mapper,
            IValidator<VolunteerCreateDTO> createValidator,
            IValidator<VolunteerUpdateDTO> updateValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(volunteerRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
        public async Task<IEnumerable<GroupSimpleDTO>> GetGroupsAsync(Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IVolunteerRepository).GetGroupsAsync(volunteerId));
            }
        }

        public async Task<IEnumerable<GroupSimpleDTO>> GetOwnedGroupsAsync(Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IVolunteerRepository).GetOwnedGroupsAsync(volunteerId));
            }
        }

        public async Task<IEnumerable<ProjectSimpleDTO>> GetOwnedProjectsAsync(Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<ProjectSimpleDTO>>(await (repository as IVolunteerRepository).GetOwnedProjectsAsync(volunteerId));
            }
        }

        public async Task<IEnumerable<RegistrationCodeListDTO>> GetOwnedRegistrationCodesAsync(Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<RegistrationCodeListDTO>>(await (repository as IVolunteerRepository).GetOwnedRegistrationCodesAsync(volunteerId));
            }
        }
    }
}
