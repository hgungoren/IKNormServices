using Abp.Application.Services;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Serendip.IK.ActivityLoggers;
using Serendip.IK.Authorization.Users;
using Serendip.IK.MultiTenancy;
using System;
using System.Threading.Tasks;

namespace Serendip.IK
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CoreAppServiceBase : ApplicationService
    { 

        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public ICacheManager CacheManager { get; set; }

        protected CoreAppServiceBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
            CacheManager = IocManager.Instance.Resolve<ICacheManager>();
        }

        public void SaveLog(string name, string displayKey, string modelName = "", string modelId = "", string referenceModel = "", string referenceId = "", string[] displayValues = null)
        {
            var ActivityLoggerAppService = IocManager.Instance.Resolve<IActivityLoggerAppService>();
            //ActivityLoggerAppService.SaveLog(new ActivityLoggers.Dto.ActivityLoggerDto
            //{
            //    Name = name,
            //    DisplayKey = displayKey,
            //    ModelName = modelName,
            //    ModelId = modelId,
            //    ReferenceModel = referenceModel,
            //    ReferenceID = referenceId,
            //    DisplayValues = displayValues
            //});  
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            if (CacheManager.GetCache("Users").GetOrDefault(AbpSession.GetUserId().ToString()) == null)
            {
                var _user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
                CacheManager.GetCache("Users").Set(AbpSession.GetUserId().ToString(), _user, slidingExpireTime: TimeSpan.FromMinutes(10));
            }
            var user = CacheManager.GetCache("Users").GetOrDefault(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user as User;
        }

        protected virtual async Task<Tenant> GetCurrentTenantAsync()
        {

            if (CacheManager.GetCache("Tenants").GetOrDefault(AbpSession.GetTenantId().ToString()) == null)
            {
                var _tenant = await TenantManager.GetByIdAsync(AbpSession.GetTenantId());

                CacheManager.GetCache("Tenants").Set(AbpSession.GetTenantId().ToString(), _tenant, slidingExpireTime: TimeSpan.FromMinutes(20));
            }
            var tenant = CacheManager.GetCache("Tenants").GetOrDefault(AbpSession.GetTenantId().ToString());

            return tenant as Tenant;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
