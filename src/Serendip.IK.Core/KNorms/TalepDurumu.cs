using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum TalepDurumu
    {
        //[Display(Name = "Bölge Operasyon Onayı Bekleniyor ")]
        //BolgeOperasyonOnayiBekleniyor,
        //[Display(Name = "Bölge Operasyon Tarafından Reddedildi")]
        //BolgeOperasyonTarafindanReddedildi,
        //[Display(Name = "Bölge Operasyon Tarafından Onaylandı, GM Operasyon Onayı Bekleniyor")]
        //BolgeOperasyonTarafindanOnaylandi_GMOperasyonOnayiBekleniyor,
        //[Display(Name = "GM Operasyon Tarafından Reddedildi")]
        //GMOperasyonTarafindanReddedildi,
        //[Display(Name = "GM Operasyon Tarafından Onaylandı, İK Yöneticisi Onayı Bekleniyor")]
        //GMOperasyonTarafindanOnaylandi_IKYoneticisiOnayiBekleniyor,
        //[Display(Name = "İK Yöneticisi Reddetti")]
        //IKYoneticisiReddetti,
        //[Display(Name = "İK Yöneticisi Onayladı, İK GMY Onayı Bekleniyor")]
        //IKYoneticisiOnayladi_IKGMYOnayiBekleniyor,
        //[Display(Name = "İK GMY Reddetti")]
        //IKGMYReddetti,
        //[Display(Name = "İK GMY Onayladı, Personel Talebi Onaylandı")]
        //IKGMYOnayladi_PersonelTalebiOnaylandi,
        NONE,
         
        BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI,

        /// <summary>
        /// Bölge Müdürü
        /// </summary>
        BOLGE_MUDURU,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdürü
        /// </summary>
        DEPARTMAN_MUDURU,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdürü
        /// </summary>
        GENEL_MUDUR_YRD,

        /// <summary>
        /// İnsan Kaynakları ve İşe Alım Müdür Yrd
        /// </summary>
        INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD,

        /// <summary>
        /// İnsan Kaynakları ve İşe Alım Müdürü
        /// </summary>
        INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU,

        /// <summary>
        /// Mikro Operasyonları Müdürü
        /// </summary>
        MIKRO_OPERASYONLARI_MUDURU,

        /// <summary>
        /// Aktarma Operasyonları Müdürü
        /// </summary>
        AKTARMA_OPERASYONLARI_MUDURU,

        /// <summary>
        /// Genel Müdür
        /// </summary>
        GENEL_MUDUR,

        /// <summary>
        /// Genel Müdürlük IK Müdür Yardımcısı
        /// </summary>
        IK_GENEL_MUDUR_YRD,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdür Yardımcısı
        /// </summary>
        OPERASYON_GENEL_MUDUR_YRD,
    }
}



