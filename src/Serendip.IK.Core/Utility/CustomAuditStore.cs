using Abp.Auditing;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Utility
{
    public class CustomAuditStore : Abp.Auditing.AuditingStore, IAuditingStore
    {
        public CustomAuditStore(IRepository<AuditLog, long> auditLogRepository) : base(auditLogRepository)
        {
        }

        public override Task SaveAsync(AuditInfo auditInfo)
        {
            Task.Run(() => {

                base.SaveAsync(auditInfo);
            });

            return Task.Delay(0);
        }
    }
}
