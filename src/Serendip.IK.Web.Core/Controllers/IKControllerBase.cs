using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Serendip.IK.Controllers
{
    public abstract class IKControllerBase: AbpController
    {
        protected IKControllerBase()
        {
            LocalizationSourceName = IKConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
