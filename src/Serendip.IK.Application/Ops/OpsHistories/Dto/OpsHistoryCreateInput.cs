using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.DamageHistory;

namespace Serendip.IK.Ops.OpsHistories.Dto
{
    [AutoMap(typeof(OpsHistroy))]
    public class OpsHistoryCreateInput
    {
        public string Islem { get; set; }
        public string Islemyapankullanici { get; set; }
        public string TazminStatu { get; set; }
        public string Odemedurumu { get; set; }
        public string GmAciklama { get; set; }
        public string BolgeAciklama { get; set; }
    }


    [AutoMap(typeof(OpsHistroy))]
    public class OpsHistoryDto : BaseEntityDto
    {
        public string Islem { get; set; }
        public string Islemyapankullanici { get; set; }
        public string TazminStatu { get; set; }
        public string Odemedurumu { get; set; }
        public string GmAciklama { get; set; }
        public string BolgeAciklama { get; set; }
    }


    public class OpsPagedKNormResultRequestDto : PagedAndSortedResultRequestDto { }

}
