using Abp.Configuration;
using Abp.Zero.Ldap.Configuration;
using Serendip.IK.Ldap.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace Serendip.IK.Ldap
{

    public interface ISuratLdapSettings : ILdapSettings
    {
        Task<string> GetTitle(int? tenantId);
    }

    public class SuratLdapSettings : ISuratLdapSettings
    {
        ISettingManager _settingManager;

        public SuratLdapSettings(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

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

        public Task<string> GetTitle(int? tenantId)
        {
            return _settingManager.GetSettingValueForApplicationAsync(SKLdapSettingNames.Title);
        }
    }
}
