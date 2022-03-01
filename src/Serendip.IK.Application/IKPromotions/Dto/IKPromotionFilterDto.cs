using Abp.AutoMapper;
using Serendip.IK.Common;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.IKPromotions.Dto
{
    [AutoMap(typeof(IKPromotion))]
    public class IKPromotionFilterDto : BaseEntityDto
    {
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string PromotionRequestTitle { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public IKPromotionType Statu { get; set; }
    }
}
