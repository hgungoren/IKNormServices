using Abp.AutoMapper;
using Serendip.IK.Common;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    [AutoMap(typeof(DamageCompensation))]
    public class DamageCompensationDto : BaseEntityDto
    {

       
      
        public string TakipNo { get; set; }
        public DateTime Sistem_InsertTime { get; set; }
        public string EvrakSeriNo { get; set; }
        public string GonderenKodu { get; set; }
        public string GonderenUnvan { get; set; }
        public string AliciKodu { get; set; }
        public string AliciUnvan { get; set; }
        public long IlkGondericiSube_ObjId { get; set; }
        public string Cikis_Sube_Unvan { get; set; }
        public long VarisSube_ObjId { get; set; }
        public string Varis_Sube_Unvan { get; set; }
        public long Birimi_ObjId { get; set; }
        public string Birimi { get; set; }
        public float Adet { get; set; }
        public int TazminStatu { get; set; }
        public string TazminStatuAd { get; set; }
        //tazmin bilgileri
        public DateTime Tazmin_Talep_Tarihi { get; set; }
        public string Tazmin_Tipi { get; set; }
        public int? Tazmin_Musteri_Tipi { get; set; }
        public string Tazmin_Musteri_Kodu { get; set; }
        public string Tazmin_Musteri_Unvan { get; set; }
        public string? Odeme_Musteri_Tipi { get; set; }
        public string TCK_NO { get; set; }
        public string VK_NO { get; set; }
        public string Odeme_Birimi_Bolge { get; set; }
        public string Odeme_Birimi_Bolge_Text { get; set; }

     

        public float Talep_Edilen_Tutar { get; set; }
        public string Surec_Sahibi_Birim_Bolge { get; set; }
        public string Surec_Sahibi_Birim_Bolge_Text { get; set; }
   

        public string Telefon { get; set; }
        public string Email { get; set; }

    


    }
}
