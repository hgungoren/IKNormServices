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


        [Display(Name = "Bölge Müdür Yrd. - Operasyon")]
        BolgeMudurYrdOperasyon = 5,


        [Display(Name = "Bölge Müdürü")]
        BolgeMuruduru = 6,


        [Display(Name = "Gm İşlemde")]
        GMIslemde = 7,


        [Display(Name = "Hasar Tazmin Uzman Yrd.")]
        HasarTazminUzmanYrd = 8,

        [Display(Name = "Hasar Tazmin Uzmanı")]
        HasarTazminUzmani = 9,

        [Display(Name = "Hasar Tazmin Müdür Yrd.")]
        HasarTazminMudurYrd = 10,

        [Display(Name = "Operasyon Müdürü")]
        OperasyonMuduru = 11,

        [Display(Name = "Genel Müdür Yrd.")]
        GenelMudurYrd = 12,


        [Display(Name = "Bölge Operasyon Uzman Yrd")]
        BolgeOperasyonUzmanYrd = 13,



        [Display(Name = "Bölge Operasyon Uzmanı")]
        BolgeOperasyonUzmani = 14,

        [Display(Name = "Satış Müdürü")]
        SatisMuduru = 15,


        [Display(Name = "Müşteri Deneyimi Müdürü")]
        MusteriDeneyimiMuduru = 16,


        [Display(Name = "Form Kapatıldı")]
        FormKapatildi = 17,
        


















    }
}
