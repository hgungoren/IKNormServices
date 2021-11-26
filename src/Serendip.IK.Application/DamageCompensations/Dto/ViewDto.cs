using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class ViewDto
    {


        public string TakipNo { get; set; }
        public string Sistem_InsertTime { get; set; }
        public string EvrakSeriNo { get; set; }
        public string GonderenKodu { get; set; }
        public string GonderenUnvan { get; set; }
        public string AliciKodu { get; set; }
        public string AliciUnvan { get; set; }
        public string IlkGondericiSube_ObjId { get; set; }
        public string Cikis_Sube_Unvan { get; set; }
        public string VarisSube_ObjId { get; set; }
        public string Varis_Sube_Unvan { get; set; }
        public string Birimi_ObjId { get; set; }
        public string Birimi { get; set; }
        public string Adet { get; set; }

        public string TazminStatu { get; set; }


        //tazmin bilgileri
        public string Tazmin_Talep_Tarihi { get; set; }
        public string Tazmin_Tipi { get; set; }
        public string Tazmin_Musteri_Tipi { get; set; }
        public string Tazmin_Musteri_Kodu { get; set; }
        public string Tazmin_Musteri_Unvan { get; set; }
        public string Odeme_Musteri_Tipi { get; set; }
        public string TCK_NO { get; set; }
        public string VK_NO { get; set; }
        public string Odeme_Birimi_Bolge { get; set; }
        public float Talep_Edilen_Tutar { get; set; }
        public string Surec_Sahibi_Birim_Bolge { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        //değerlendirme 

        public string TazminId { get; set; }
        public string EvaTazmin_Tipi { get; set; }
        public string EvaTazmin_Nedeni { get; set; }
        public string EvaKargo_Bulundugu_Yer { get; set; }
        public string EvaKusurlu_Birim { get; set; }
        public string EvaIcerik_Grubu { get; set; }
        public string EvaIcerik { get; set; }
        public string EvaUrun_Aciklama { get; set; }
        public string EvaEkleyen_Kullanici { get; set; }
        public string EvaBolge_Aciklama { get; set; }
        public string EvaGm_Aciklama { get; set; }
        public string EvaTalep_Edilen_Tutar { get; set; }
        public string EvaTazmin_Odeme_Durumu { get; set; }
        public string EvaOdenecek_Tutar { get; set; }


        public string Meseaj { get; set; }
        public bool IsError { get; set; }

    }
}
