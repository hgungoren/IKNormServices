using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.IKPromotions.Dto
{
    public class PagedIKPromotionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? isActive { get; set; }
    }
}
