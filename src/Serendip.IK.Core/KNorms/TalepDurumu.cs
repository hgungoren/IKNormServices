using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum TalepDurumu
    {
        [Display(Name = "Bölge Operasyon Onayı Bekleniyor ")]
        BolgeOperasyonOnayiBekleniyor,
        [Display(Name = "Bölge Operasyon Tarafından Reddedildi")]
        BolgeOperasyonTarafindanReddedildi,
        [Display(Name = "Bölge Operasyon Tarafından Onaylandı, GM Operasyon Onayı Bekleniyor")]
        BolgeOperasyonTarafindanOnaylandi_GMOperasyonOnayiBekleniyor,
        [Display(Name = "GM Operasyon Tarafından Reddedildi")]
        GMOperasyonTarafindanReddedildi,
        [Display(Name = "GM Operasyon Tarafından Onaylandı, İK Yöneticisi Onayı Bekleniyor")]
        GMOperasyonTarafindanOnaylandi_IKYoneticisiOnayiBekleniyor,
        [Display(Name = "İK Yöneticisi Reddetti")]
        IKYoneticisiReddetti,
        [Display(Name = "İK Yöneticisi Onayladı, İK GMY Onayı Bekleniyor")]
        IKYoneticisiOnayladi_IKGMYOnayiBekleniyor,
        [Display(Name = "İK GMY Reddetti")]
        IKGMYReddetti,
        [Display(Name = "İK GMY Onayladı, Personel Talebi Onaylandı")]
        IKGMYOnayladi_PersonelTalebiOnaylandi
    }
}



