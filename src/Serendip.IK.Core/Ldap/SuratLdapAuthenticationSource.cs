using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Serendip.IK.Authorization.Users;
using Serendip.IK.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ldap
{
    public class SuratLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public SuratLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
           : base(settings, ldapModuleConfig)
        {
        }
    }
}
