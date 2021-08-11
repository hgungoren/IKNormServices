using Abp.AutoMapper;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Periods.Dto
{
    [AutoMap(typeof(Period))]
    public class PeriodDto : BaseEntityDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
