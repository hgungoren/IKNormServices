using Abp.MultiTenancy;
using Serendip.IK.Authorization.Users;

namespace Serendip.IK.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
