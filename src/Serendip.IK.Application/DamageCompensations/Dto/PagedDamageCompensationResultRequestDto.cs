using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class PagedDamageCompensationResultRequestDto: PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
