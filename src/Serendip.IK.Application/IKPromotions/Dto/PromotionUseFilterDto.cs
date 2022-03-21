using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.IKPromotions.Dto
{
    public class PromotionUseFilterDto
    {
        public IKPromotionType Statu { get; set; }
        public string Title { get; set; }
        public string PromotionRequestTitle { get; set; }
        public DateTime FirstRequestDate { get; set; }
        public DateTime SecondRequestDate { get; set; }
        public long DepartmentObjId { get; set; }
        public long UnitObjId { get; set; }
    }
}
