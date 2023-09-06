using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.DTO.RegistrationCode;
using GroupBy.Design.DTO.Volunteer;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class VolunteerService : AsyncService<Volunteer, VolunteerSimpleDTO, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>, IVolunteerService
    {
        private readonly IRankRepository rankRepository;

        public VolunteerService(
            IVolunteerRepository volunteerRepository,
            IRankRepository rankRepository,
            IMapper mapper,
            IValidator<VolunteerCreateDTO> createValidator,
            IValidator<VolunteerUpdateDTO> updateValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(volunteerRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.rankRepository = rankRepository;
        }

        public override async Task<VolunteerDTO> GetAsync(VolunteerSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<VolunteerDTO>(await repository.GetAsync(mapper.Map<Volunteer>(model), includes: "Rank"));
            }
        }

        protected override async Task<Volunteer> CreateOperationAsync(VolunteerCreateDTO model)
        {
            var entity = mapper.Map<Volunteer>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.Rank != null)
                    entity.Rank = await rankRepository.GetAsync(entity.Rank);

                var createdVolunteer = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdVolunteer;
            }
        }

        protected override async Task<Volunteer> UpdateOperationAsync(Volunteer entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.Rank != null)
                    entity.Rank = await rankRepository.GetAsync(entity.Rank);

                var updatedVolunteer = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedVolunteer;
            }
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
