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
        #region Constructor
        ISettingManager _settingManager;

        public SuratLdapSettings(ISettingManager settingManager)
        {
            this._settingManager = settingManager;
        }
        #endregion

        #region GetIsEnabled
        public async Task<bool> GetIsEnabled(int? tenantId)
        {
            return true;
        }
        #endregion

        #region GetContextType
        public async Task<ContextType> GetContextType(int? tenantId)
        {
            return ContextType.Domain;
        }
        #endregion

        #region GetContainer
        public async Task<string> GetContainer(int? tenantId)
        {
            return null;
        }
        #endregion

        #region GetDomain
        public async Task<string> GetDomain(int? tenantId)
        {
            return null;
        }
        #endregion

        #region GetUserName
        public async Task<string> GetUserName(int? tenantId)
        {
            return null;
        }
        #endregion

        #region GetPassword
        public async Task<string> GetPassword(int? tenantId)
        {
            return null;
        }
        #endregion

        #region GetTitle
        public Task<string> GetTitle(int? tenantId)
        {
            return _settingManager.GetSettingValueForApplicationAsync(SKLdapSettingNames.Title);
        } 
        #endregion
    }
}
