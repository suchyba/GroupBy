using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Identity
{
    public class ApplicationRoleStore : IRoleStore<IdentityRole<Guid>>, IRoleClaimStore<IdentityRole<Guid>>
    {
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;
        private readonly IRoleRepository<Guid> roleRepository;
        private readonly IRoleClaimRepository<Guid> roleClaimRepository;

        public ApplicationRoleStore(IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory, IRoleRepository<Guid> roleRepository, IRoleClaimRepository<Guid> roleClaimRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.roleRepository = roleRepository;
            this.roleClaimRepository = roleClaimRepository;
        }

        public async Task AddClaimAsync(IdentityRole<Guid> role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var roleClaimEntity = new IdentityRoleClaim<Guid>
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    RoleId = role.Id
                };

                await roleClaimRepository.CreateAsync(roleClaimEntity);

                await uow.Commit();
            }
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await roleRepository.CreateAsync(role);

                await uow.Commit();
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await roleRepository.DeleteAsync(role);

                await uow.Commit();
                return IdentityResult.Success;
            }
        }

        public void Dispose()
        {

        }

        public async Task<IdentityRole<Guid>> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (!Guid.TryParse(roleId, out Guid id))
                    throw new BadRequestException($"RoleId is in not valid format {roleId}");

                return await roleRepository.GetAsync(new IdentityRole<Guid> { Id = id });
            }
        }

        public async Task<IdentityRole<Guid>> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return await roleRepository.GetByNameAsync(normalizedRoleName);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityRole<Guid> role, CancellationToken cancellationToken = default)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return (await roleClaimRepository.GetByRoleIdAsync(role.Id)).Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            }
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.Name);
        }

        public async Task RemoveClaimAsync(IdentityRole<Guid> role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var roleClaimEntity = (await roleClaimRepository.GetByRoleIdAsync(role.Id)).FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

                if (roleClaimEntity != null)
                {
                    await roleClaimRepository.DeleteAsync(new IdentityRoleClaim<Guid> { Id = roleClaimEntity.Id });
                    await uow.Commit();
                }
                else
                {
                    throw new NotFoundException(roleClaimEntity.GetType().Name, new
                    {
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value
                    });
                }
            }
        }

        public async Task SetNormalizedRoleNameAsync(IdentityRole<Guid> role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var dbRole = await roleRepository.GetAsync(role);

                dbRole.NormalizedName = normalizedName;
                await roleRepository.UpdateAsync(dbRole);

                await uow.Commit();
            }
        }

        public async Task SetRoleNameAsync(IdentityRole<Guid> role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var dbRole = await roleRepository.GetAsync(role);

                dbRole.Name = roleName;
                await roleRepository.UpdateAsync(dbRole);

                await uow.Commit();
            }
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole<Guid> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await roleRepository.UpdateAsync(role);
                await uow.Commit();

                return IdentityResult.Success;
            }
        }
    }
}
