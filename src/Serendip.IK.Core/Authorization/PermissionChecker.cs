using Abp.Authorization;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;

namespace Serendip.IK.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
