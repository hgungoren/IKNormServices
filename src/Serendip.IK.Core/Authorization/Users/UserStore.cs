using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Serendip.IK.Authorization.Roles;

namespace Serendip.IK.Authorization.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UserStore(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserClaim, long> userClaimRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(unitOfWorkManager,
                  userRepository,
                  roleRepository,
                  userRoleRepository,
                  userLoginRepository,
                  userClaimRepository,
                  userPermissionSettingRepository,
                  userOrganizationUnitRepository,
                  organizationUnitRoleRepository
                  )
        {
            _unitOfWorkManager = unitOfWorkManager;
        }



        public User GetUserByEmailOrUsernameWithDisableTenant(string emailOrUsername)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                return UserRepository.FirstOrDefault(x => x.EmailAddress == emailOrUsername || x.UserName == emailOrUsername);
            }
        }

        public User GetUserByEmailOrUsername(string emailOrUsername)
        {
            return UserRepository.FirstOrDefault(x => x.EmailAddress == emailOrUsername || x.UserName == emailOrUsername);
        }
    }
}
