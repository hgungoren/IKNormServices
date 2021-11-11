using Abp.Zero.Ldap.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ldap
{
    public class SuratLdapSettings : ILdapSettings
    {
        public async Task<bool> GetIsEnabled(int? tenantId)
        {
            return true;
        }

        public async Task<ContextType> GetContextType(int? tenantId)
        {
            return ContextType.Domain;
        }

        public async Task<string> GetContainer(int? tenantId)
        {
            return null;
        }

        public async Task<string> GetDomain(int? tenantId)
        {
            return null;
        }

        public async Task<string> GetUserName(int? tenantId)
        {
            return null;
        }

        public async Task<string> GetPassword(int? tenantId)
        {
            return null;
        }
    }
}
