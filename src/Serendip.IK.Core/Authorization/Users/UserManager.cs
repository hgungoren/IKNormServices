using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {

        private readonly IAbpSession _abpSession;
        private readonly TenantManager tenantManager;
        private readonly UserStore _store;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISettingManager _settingManager;


        public UserManager(
        RoleManager roleManager,
        UserStore store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger,
        IPermissionManager permissionManager,
        IUnitOfWorkManager unitOfWorkManager,
        ICacheManager cacheManager,
        IRepository<OrganizationUnit, long> organizationUnitRepository,
        IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
        IOrganizationUnitSettings organizationUnitSettings,
        ISettingManager settingManager,
        IAbpSession abpSession, TenantManager tenantManager)
        : base(
            roleManager,
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger,
            permissionManager,
            unitOfWorkManager,
            cacheManager,
            organizationUnitRepository,
            userOrganizationUnitRepository,
            organizationUnitSettings,
            settingManager)
        {
            _abpSession = abpSession;
            this.tenantManager = tenantManager;
            _store = store;
            _unitOfWorkManager = unitOfWorkManager;
            _settingManager = settingManager;
        }



        public async Task<string> GetCurrentUserName()
        {
            var user = await FindByIdAsync(_abpSession.GetUserId().ToString());
            return user.UserName;
        }
        public string GetUserSettingByName(string settingName, int? tenantId, long userId)
        {
            var settingValue = _settingManager.GetSettingValueForUser("Serendip.IK.SendEmailOnNotification", tenantId, userId);
            return settingValue;
        }
        public User GetUserByEmailOrUsernameWithDisableTenant(string emailOrUsername)
        {
            return _store.GetUserByEmailOrUsernameWithDisableTenant(emailOrUsername);
        }
        public User GetUserByEmailOrUsername(string emailOrUsername)
        {
            return _store.GetUserByEmailOrUsername(emailOrUsername);
        }

        [UnitOfWork]
        public virtual User GetUserByEmailOrUsernameWithTenant(string emailOrUsername, string tenancyName)
        {
            var tenant = tenantManager.FindByTenancyName(tenancyName);
            if (tenant == null)
            {
                return null;
            }
            using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
            {
                var user = _store.GetUserByEmailOrUsername(emailOrUsername);
                return user;
            }
        }
        public static bool IsAdmin()
        {
            var session = IocManager.Instance.Resolve<IAbpSession>();
            var self = IocManager.Instance.Resolve<UserManager>();

            var user = self.FindByIdAsync(session.GetUserId().ToString()).Result;
            var roles = self.GetRolesAsync(user).Result;

            return roles.Any(x => x == "Admin");
        }
    }
}
