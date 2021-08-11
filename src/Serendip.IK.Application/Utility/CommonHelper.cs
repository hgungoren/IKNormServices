using System;
using System.Collections.Generic;
using System.Text;
using Abp.Dependency;
using Microsoft.AspNetCore.DataProtection;

namespace Serendip.IK.Utility
{
    public class CommonHelper
    {
        public static string GetEmailDomain(string email)
        {
            var domains = new string[] { "gmail", "aol", "outlook", "zoho", "mail", "yahoo", "proton", "icloud", "gmx", "yandex", "hotmail", "windowslive", "tutanota", "lycos" };
            var emailDomain = email.Split("@")[1].ToLower();
            foreach (var domain in domains)
            {
                if (emailDomain.Contains(domain))
                {
                    return String.Empty;
                }
            }
            return emailDomain.Split(".")[0];
        }
        private const string Key = "Q#6sg36Kk*tmg*$JyfuyqR54uacqat";
        public static string Encrypt(string input)
        {
            var dataProtectionProvider = IocManager.Instance.Resolve<IDataProtectionProvider>();
            var protector = dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public static string Decrypt(string cipherText)
        {
            var dataProtectionProvider = IocManager.Instance.Resolve<IDataProtectionProvider>();
            var protector = dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
