using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Identity
{
    public class ApplicationUserStore :
        IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserLoginStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>,
        IUserClaimStore<ApplicationUser>,
        IUserAuthenticationTokenStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>,
        IUserLockoutStore<ApplicationUser>,
        IQueryableUserStore<ApplicationUser>
    {
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;
        private readonly IApplicationUserRepository userRepository;
        private readonly IUserLoginRepository<Guid> userLoginRepository;
        private readonly IUserRoleRepository<Guid> userRoleRepository;
        private readonly IRoleRepository<Guid> roleRepository;
        private readonly IUserClaimRepository<Guid> userClaimRepository;
        private readonly IUserTokenRepository<Guid> userTokenRepository;

        public ApplicationUserStore(
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory,
            IApplicationUserRepository userRepository,
            IUserLoginRepository<Guid> userLoginRepository,
            IUserRoleRepository<Guid> userRoleRepository,
            IRoleRepository<Guid> roleRepository,
            IUserClaimRepository<Guid> userClaimRepository,
            IUserTokenRepository<Guid> userTokenRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.userRepository = userRepository;
            this.userLoginRepository = userLoginRepository;
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
            this.userClaimRepository = userClaimRepository;
            this.userTokenRepository = userTokenRepository;
        }
        public IQueryable<ApplicationUser> Users
        {
            get
            {
                using (var uow = unitOfWorkFactory.CreateUnitOfWork())
                {
                    return userRepository.GetAll().AsQueryable();
                }
            }
        }

        public async Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                foreach (var claim in claims)
                {
                    await userClaimRepository.CreateAsync(new IdentityUserClaim<Guid>
                    {
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value,
                        UserId = user.Id
                    });
                }

                await uow.Commit();
            }
        }

        public async Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var loginEntity = new IdentityUserLogin<Guid>
                {
                    LoginProvider = login.LoginProvider,
                    ProviderDisplayName = login.ProviderDisplayName,
                    ProviderKey = login.ProviderKey,
                    UserId = user.Id
                };

                await userLoginRepository.CreateAsync(loginEntity);

                await uow.Commit();
            }
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var role = await roleRepository.GetByNameAsync(roleName);

                await userRoleRepository.CreateAsync(new IdentityUserRole<Guid> { RoleId = role.Id, UserId = user.Id });

                await uow.Commit();
            }
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await userRepository.CreateAsync(user);

                await uow.Commit();
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await userRepository.DeleteAsync(user);

                await uow.Commit();
                return IdentityResult.Success;
            }
        }

        public void Dispose()
        {

        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return await userRepository.GetByNormalizedEmailAsync(normalizedEmail);
            }
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (!Guid.TryParse(userId, out Guid id))
                    throw new BadRequestException($"{userId} is not a valid GUID");

                return await userRepository.GetAsync(new { Id = id });
            }
        }

        public async Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var login = await userLoginRepository.GetAsync(new IdentityUserLogin<Guid> { ProviderKey = providerKey, LoginProvider = loginProvider });

                return await userRepository.GetAsync(new ApplicationUser { Id = login.UserId });
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                try
                {
                    return await userRepository.GetByNormalizedUserNameAsync(normalizedUserName);
                }
                catch (NotFoundException)
                {
                    return await Task.FromResult<ApplicationUser>(null);
                }
            }
        }

        public async Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return (await userRepository.GetAsync(user)).AccessFailedCount;
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return (await userClaimRepository.GetByUserIdAsync(user.Id)).Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            }
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEnd);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return (await userLoginRepository.GetByUserIdAsync(user.Id))
                    .Select(i => new UserLoginInfo(i.LoginProvider, i.ProviderKey, i.ProviderDisplayName))
                    .ToList(); ;
            }
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var roleIds = await userRoleRepository.GetRoleIdsByUserIdAsync(user.Id);

                List<string> roleNames = new List<string>();

                foreach (var roleId in roleIds)
                    roleNames.Add((await roleRepository.GetAsync(new IdentityRole<Guid> { Id = roleId })).Name);

                return roleNames;
            }
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.SecurityStamp);
        }

        public async Task<string> GetTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return (await userTokenRepository.GetAsync(new IdentityUserToken<Guid>
                {
                    UserId = user.Id,
                    LoginProvider = loginProvider,
                    Name = name,
                })).Name;
            }
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.UserName);
        }

        public async Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var userIds = await userClaimRepository.GetUserIdsForClaimAsync(claim.Type, claim.Value);

                List<ApplicationUser> users = new List<ApplicationUser>();

                foreach (var userId in userIds)
                {
                    users.Add(await userRepository.GetAsync(new ApplicationUser { Id = userId }));
                }

                return users;
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var role = await roleRepository.GetByNameAsync(roleName);

                var userIds = await userRoleRepository.GetUserIdsByRoleIdAsync(role.Id);

                List<ApplicationUser> users = new List<ApplicationUser>();

                foreach (var userId in userIds)
                    users.Add((await userRepository.GetAsync(new IdentityRole<Guid> { Id = userId })));

                return users;
            }
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public async Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var dbUser = await userRepository.GetAsync(user);

                int count = ++dbUser.AccessFailedCount;

                await userRepository.UpdateAsync(dbUser);

                await uow.Commit();
                return count;
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var role = await roleRepository.GetByNameAsync(roleName);

                var userRoles = await userRoleRepository.GetRoleIdsByUserIdAsync(user.Id);

                return userRoles.Any(r => r == role.Id);
            }
        }

        public async Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var userClaimEntities = await userClaimRepository.GetByUserIdAsync(user.Id);

                foreach (var claim in claims)
                {
                    var userClaimEntity = userClaimEntities.SingleOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
                    await userClaimRepository.DeleteAsync(userClaimEntity);
                }

                await uow.Commit();
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var role = await roleRepository.GetByNameAsync(roleName);

                var userRole = await userRoleRepository.GetAsync(new IdentityUserRole<Guid> { UserId = user.Id, RoleId = role.Id });

                await userRoleRepository.DeleteAsync(userRole);

                await uow.Commit();
            }
        }

        public async Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var userLogin = await userLoginRepository.GetAsync(new IdentityUserLogin<Guid> { LoginProvider = loginProvider, ProviderKey = providerKey });

                await userLoginRepository.DeleteAsync(userLogin);

                await uow.Commit();
            }
        }

        public async Task RemoveTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var token = await userTokenRepository.GetAsync(new IdentityUserToken<Guid>
                {
                    LoginProvider = loginProvider,
                    UserId = user.Id,
                    Name = name
                });

                await userTokenRepository.DeleteAsync(token);
                await uow.Commit();
            }
        }

        public async Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var userClaim = (await userClaimRepository.GetByUserIdAsync(user.Id)).FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

                if (userClaim == null)
                    throw new NotFoundException(claim.GetType().Name, new { ClaimType = claim.Type, ClaimValue = claim.Value });

                userClaim.ClaimType = newClaim.Type;
                userClaim.ClaimValue = newClaim.Value;

                await userClaimRepository.UpdateAsync(userClaim);
                await uow.Commit();
            }
        }

        public async Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var dbUser = await userRepository.GetAsync(user);

                dbUser.AccessFailedCount = 0;

                await userRepository.UpdateAsync(dbUser);

                await uow.Commit();
            }
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEnd = lockoutEnd;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public async Task SetTokenAsync(ApplicationUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var userTokenEntity = new IdentityUserToken<Guid>
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value,
                    UserId = user.Id
                };

                await userTokenRepository.CreateAsync(userTokenEntity);
                await uow.Commit();
            }
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.TwoFactorEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await userRepository.UpdateAsync(user);

                await uow.Commit();
                return IdentityResult.Success;
            }
        }
    }
}
