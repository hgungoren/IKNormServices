using Abp.Domain.Entities;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Periods
{
    public class Period : BaseEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
