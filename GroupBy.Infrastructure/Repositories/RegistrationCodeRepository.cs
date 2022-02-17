using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RegistrationCodeRepository : AsyncRepository<RegistrationCode>, IRegistrationCodeRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IRankRepository rankRepository;
        private readonly IVolunteerRepository volunteerRepository;

        public RegistrationCodeRepository(DbContext context, IGroupRepository groupRepository, IRankRepository rankRepository, IVolunteerRepository volunteerRepository) : base(context)
        {
            this.groupRepository = groupRepository;
            this.rankRepository = rankRepository;
            this.volunteerRepository = volunteerRepository;
        }

        public override async Task<RegistrationCode> GetAsync(RegistrationCode domain)
        {
            RegistrationCode registrationCode = await context.Set<RegistrationCode>()
                .Include(c => c.TargetGroup)
                .Include(c => c.TargetRank)
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Code == domain.Code);

            if (registrationCode == null)
                throw new NotFoundException("RegistrationCode", domain.Code);

            return registrationCode;
        }

        public override Task<RegistrationCode> UpdateAsync(RegistrationCode domain)
        {
            throw new BadRequestException("Operation not allowed");
        }
        public override async Task<RegistrationCode> CreateAsync(RegistrationCode domain)
        {
            domain.TargetGroup = await groupRepository.GetAsync(domain.TargetGroup);
            if (domain.TargetRank != null)
                domain.TargetRank = await rankRepository.GetAsync(domain.TargetRank);
            domain.Owner = await volunteerRepository.GetAsync(domain.Owner);

            if (!await groupRepository.IsMember(domain.TargetGroup.Id, domain.Owner.Id))
                throw new BadRequestException("Creator of registration code must be a member of the target group");

            var newCode = await context.Set<RegistrationCode>().AddAsync(domain);

            await context.SaveChangesAsync();
            return newCode.Entity;
        }
    }
}
