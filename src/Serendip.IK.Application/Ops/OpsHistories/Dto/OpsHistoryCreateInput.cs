using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.Common;
<<<<<<< HEAD
using Serendip.IK.Ops.History;
=======
using Serendip.IK.Ops.DamageHistory;
>>>>>>> 03d11f9260b3faeeac3de87892ee31b75e9ec3b6

namespace Serendip.IK.Ops.OpsHistories.Dto
{
    [AutoMap(typeof(OpsHistroy))]
    public class OpsHistoryCreateInput
    {
<<<<<<< HEAD

        public long TazminId { get; set; }
=======
>>>>>>> 03d11f9260b3faeeac3de87892ee31b75e9ec3b6
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
<<<<<<< HEAD

        public long TazminId { get; set; }
=======
>>>>>>> 03d11f9260b3faeeac3de87892ee31b75e9ec3b6
        public string Islem { get; set; }
        public string Islemyapankullanici { get; set; }
        public string TazminStatu { get; set; }
        public string Odemedurumu { get; set; }
        public string GmAciklama { get; set; }
        public string BolgeAciklama { get; set; }
    }


    public class OpsPagedKNormResultRequestDto : PagedAndSortedResultRequestDto { }

}
