using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum TazminStatu
    {
        [Display(Name = "Taslak")]
        Taslak = 1,

        [Display(Name = "Tazmin Eksik Evrak")]
        TazminEksikEvrak = 2,


        [Display(Name = "Tazmin Oluşturuldu")]
        TazminOlusturuldu = 3,


        [Display(Name = "Bölge İşlemde")]
        BolgeIslemde = 4,


        [Display(Name = "Operasyon Bölge Müdür Yardımcısı Onayında")]
        OperasyonBolgeMudurYardımcısıOnayında = 5,


        [Display(Name = "Bölge Müdürü Onayında")]
        BolgeMuduruOnayında = 6,

        [Display(Name = "Operasyon GMY Onayında")]
        OperasyonGMYOnayında =7,


        [Display(Name = "GM Satış Müdürü Onayında")]
        GmSatisMuduruOnayında = 8,


        [Display(Name = "GM Müşteri İlişkileri Müdürü Onayında")]
        GmMusteriIliskileriMuduruOnayında = 9,

        [Display(Name = "Satış GMY Onayında")]
        SatisGMYOnayında = 10,


        [Display(Name = "Tazmin Formu Onaylandı")]
        TazminFormuOnaylandi = 11,



    }
}
