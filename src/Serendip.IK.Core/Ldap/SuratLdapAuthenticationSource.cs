using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using ADExtended;
using Serendip.IK.Authorization.Users;
using Serendip.IK.MultiTenancy;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace ADExtended
{
    public class UserPrincipalExSearchFilter : AdvancedFilters
    {
        public UserPrincipalExSearchFilter(Principal p) : base(p) { }

        public void LogonCount(int value, MatchType mt)
        {
            this.AdvancedFilterSet("LogonCount", value, typeof(int), mt);
        }
    }

    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("User")]
    public class UserPrincipalEx : UserPrincipal
    {
        public UserPrincipalEx(PrincipalContext context) : base(context) { }
        public UserPrincipalEx(PrincipalContext context,
                             string samAccountName,
                             string password,
                             bool enabled) : base(context, samAccountName, password, enabled) { }

        UserPrincipalExSearchFilter searchFilter;
        new public UserPrincipalExSearchFilter AdvancedSearchFilter
        {
            get
            {
                if (null == searchFilter)
                    searchFilter = new UserPrincipalExSearchFilter(this);

                return searchFilter;
            }
        }

        [DirectoryProperty("title")]
        public string Title
        {
            get
            {
                if (ExtensionGet("title").Length != 1)
                    return string.Empty;

                return (string)ExtensionGet("title")[0];
            }
            set { ExtensionSet("title", value); }
        }

        /// <summary>
        /// homePhone Alanına Sicil Numarası Eklenmiştir
        /// </summary>
        [DirectoryProperty("homePhone")]
        public string SicilNo
        {
            get
            {
                if (ExtensionGet("homePhone").Length != 1)
                    return "0";

                return (string)ExtensionGet("homePhone")[0];
            }
            set { ExtensionSet("homePhone", value); }
        }


        public static new UserPrincipalEx FindByIdentity(PrincipalContext context, string identityValue)
        {
            return (UserPrincipalEx)FindByIdentityWithType(context, typeof(UserPrincipalEx), identityValue);
        }

        public static new UserPrincipalEx FindByIdentity(PrincipalContext context, IdentityType identityType, string identityValue)
        {
            return (UserPrincipalEx)FindByIdentityWithType(context, typeof(UserPrincipalEx), identityType, identityValue);
        }
    }
}
 

namespace Serendip.IK.Ldap
{
    public class SuratLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ISuratLdapSettings _settings;

        public SuratLdapAuthenticationSource(ISuratLdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig) : base(settings, ldapModuleConfig)
        {
            this._settings = settings;
            this._ldapModuleConfig = ldapModuleConfig;
        }

        protected override void UpdateUserFromPrincipal(User user, UserPrincipal userPrincipal)
        { 
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipalEx myUser = UserPrincipalEx.FindByIdentity(ctx, userPrincipal.Name);
                if (myUser != null)
                {
                    user.Title = myUser.Title;
                    user.SicilNo =   int.Parse(myUser.SicilNo);
                }
            }

            base.UpdateUserFromPrincipal(user, userPrincipal);
        }

        protected override Task<PrincipalContext> CreatePrincipalContext(Tenant tenant, User user)
        {
            return base.CreatePrincipalContext(tenant, user);
        }

        protected override Task<PrincipalContext> CreatePrincipalContext(Tenant tenant)
        {
            return base.CreatePrincipalContext(tenant);
        }

        protected override Task<PrincipalContext> CreatePrincipalContext(Tenant tenant, string userNameOrEmailAddress)
        {
            return base.CreatePrincipalContext(tenant, userNameOrEmailAddress);
        }

        public override Task<User> CreateUserAsync(string userNameOrEmailAddress, Tenant tenant)
        {
            return base.CreateUserAsync(userNameOrEmailAddress, tenant);
        }
    }
}
