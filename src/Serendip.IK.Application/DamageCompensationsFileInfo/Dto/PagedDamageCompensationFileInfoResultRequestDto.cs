using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsFileInfo.Dto
{
     public class PagedDamageCompensationFileInfoResultRequestDto :PagedAndSortedResultRequestDto
    
    {
        public string Name { get; set; }
    }
}
