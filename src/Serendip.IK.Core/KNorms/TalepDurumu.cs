using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum TalepDurumu
    {
        [Display(Name = "None")]
        NONE,

        /// <summary>
        /// Bölge Müdür Yardımcısı Insan Kaynakları
        /// </summary>
        [Display(Name = "bölge müdür yrd. - insan kaynakları")]
        BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI,  
             

        /// <summary>
        /// Bölge Müdürü
        /// </summary>
        [Display(Name = "bölge müdürü")]
        BOLGE_MUDURU,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdürü
        /// </summary>
        [Display(Name = "departman müdürü")]
        DEPARTMAN_MUDURU,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdürü
        /// </summary>
        [Display(Name = "genel müdür yrd")]
        GENEL_MUDUR_YRD,

        /// <summary>
        /// İnsan Kaynakları ve İşe Alım Müdür Yrd
        /// </summary>
        [Display(Name = "insan kaynaklari ve ise alim mudur yrd")]
        INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD,

        /// <summary>
        /// İnsan Kaynakları ve İşe Alım Müdürü
        /// </summary>
        [Display(Name = "insan kaynaklari ve ise alim müdürü")]
        INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU,

        /// <summary>
        /// Mikro Operasyonları Müdürü
        /// </summary>
        [Display(Name = "mikro operasyonlari müdürü")]
        MIKRO_OPERASYONLARI_MUDURU,

        /// <summary>
        /// Aktarma Operasyonları Müdürü
        /// </summary>
        [Display(Name = "aktarma operasyonlari müdürü")]
        AKTARMA_OPERASYONLARI_MUDURU,

        /// <summary>
        /// Genel Müdür
        /// </summary>
        [Display(Name = "genel müdür")]
        GENEL_MUDUR,

        /// <summary>
        /// Genel Müdürlük IK Müdür Yardımcısı
        /// </summary>
        [Display(Name = "insan kaynakları genel müdür yrd")]
        INSAN_KAYNAKLARI_GENEL_MUDUR_YRD,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdür Yardımcısı
        /// </summary>
        [Display(Name = "operasyon genel müdür yrd")]
        OPERASYON_GENEL_MUDUR_YRD,

        /// <summary>
        /// Genel Müdürlük Operasyon Müdürü
        /// </summary>
        [Display(Name = "operasyon müdürü")]
        OPERASYON_MUDURU,

        /// <summary>
        /// Tüm Onaylar Başarıyla Sonlandığında
        /// </summary>
        [Display(Name = "onaylandı sonlandı")]
        ONAYLANDI_SONLANDI 
    }
}



