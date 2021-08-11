using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.Emails
{
    public class EmailLink : BaseEntity, IMustHaveTenant
    {
        public long EmailId { get; set; }
        public Email Email { get; set; }

        public string Url { get; set; }
        public int TenantId { get; set; }
    }
}