using System.Linq;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.ProviderAccounts;

namespace Serendip.IK.EntityFrameworkCore.Seed.Host
{
    public class DefaultProviderCreator
    {
        private readonly IKDbContext _context;

        public DefaultProviderCreator(IKDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateProviders();
        }

        private void CreateProviders()
        {
            var defaultProvider = _context.ProviderAccounts.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "Bilgilendirme");
            if (defaultProvider == null)
            {
                defaultProvider = new ProviderAccount
                {
                    Name = "Bilgilendirme",
                    Type = "smtp",
                    Provider = "mail.suratkargo.com.tr",
                    TenantId = 1,
                    AuthorizeLevel = Authorization.AuthorizeLevel.Public,
                    OwnerId = 1, 
                };
                _context.ProviderAccounts.Add(defaultProvider);
                _context.SaveChanges();
            }
        }
    }
}
