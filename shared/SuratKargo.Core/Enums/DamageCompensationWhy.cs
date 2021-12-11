using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum DamageCompensationWhy : byte
    {

        //HASAR
        [Display(Name = "Taşımadan Kaynaklı")]
        tasimadankaynakli = 0,


        [Display(Name = "İstif Hatası")]
        istifhatasi = 1,

        [Display(Name = "Kaza")]
        kaza = 2,

        [Display(Name = "Teslimat Esnasında Tespit-DTT Var")]
        teslimatesnasındatespitdttvar = 3,

        [Display(Name = "Teslimattan Sonra-DTT")]
        teslimatsonradtt = 4,

        [Display(Name = "Aracın Su Alması")]
        aracınsualmasi = 5,

        [Display(Name = "Banttan Düşme")]
        banttandusme = 6,

        [Display(Name = "Farklı Kargonun Zarar Vermesi")]
        farklıkargonunzararvermesi = 7,

        [Display(Name = "Ambalaj Yetersizliği")]
        ambalajyetersizligi = 8,

        [Display(Name = "Doğal Afet")]
        dogalafet = 9,

        [Display(Name = "Müşteri Memnuniyeti")]
        musterimemnuniyeti = 10,

        //KAYIP
        [Display(Name = "Adres Teslim Sırasında Kayıp")]
        adresteslimsirasindakayip = 11,

        [Display(Name = "Aktarma-Aktarma Arasında")]
        aktarmaaktarmaarasinda = 12,

        [Display(Name = "Faturası Düzenlenmeden Kayıp")]
        faturasiduzeunlemedekayıp = 13,

        [Display(Name = "Gasp")]
        gasp = 14,

        [Display(Name = "İçten Eksilme")]
        iscteneksilme = 15,

        [Display(Name = "Kaza")]
        kkaza = 16,

        [Display(Name = "Şube Kayıp")]
        subekayip = 17,

        [Display(Name = "Birim-Aktarma Arasında Kayıp")]
        birimaktarmaarasindakayip = 18,

        [Display(Name = "Teslim Belgesi Sunulamaması")]
        teslimbelgesisunulmasi = 19,

        [Display(Name = "Yanlış Kişiye Teslimat")]
        yanliskisiyeteslimat = 20,

        [Display(Name = "Müşteri Memnuniyeti")]
        kmusterimemnuniyeti = 21,

        // GEC TESLİMAT
        [Display(Name = "Geç Teslim")]
        gecteslimat = 22,

        //MusteriMemnuniyeti
        [Display(Name = "Müşteri Memnuniyeti")]
        mmusterimemnuniyeti = 23,




    }
}
