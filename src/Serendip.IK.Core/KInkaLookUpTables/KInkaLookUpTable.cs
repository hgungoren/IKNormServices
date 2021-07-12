using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.KInkaLookUpTables
{
    public class KInkaLookUpTable : BaseEntity
    {
        public long ObjId { get; set; }
        public byte[] Sistem_Timestamp { get; set; }
        public string Aciklama { get; set; }
        public string Adi { get; set; }
        public bool? Aktif { get; set; }
        public byte IsOther { get; set; }
        public string Kodu { get; set; }
        public string ListName { get; set; }
        public long? Sirketi_ObjId { get; set; }
        public string Sistem_InsertLogin { get; set; }
        public System.DateTime? Sistem_InsertTime { get; set; }
        public string Sistem_TransactionId { get; set; }
        public string Sistem_UpdateLogin { get; set; }
        public System.DateTime? Sistem_UpdateTime { get; set; }
        public string Aciklama1 { get; set; }
        public string ParentKodu { get; set; }
        public string Sistem_UpdateTerminal { get; set; }
        public string Sistem_InsertTerminal { get; set; }
        public int? SiraNo { get; set; }
        public bool? BolgeHakki { get; set; }
        public string MeslekKodu { get; set; }
        public string MeslekAdi { get; set; }
        public string BordroKarsiligi { get; set; }
        public long? EksikGunNedeni_ObjId { get; set; }
        public int? MailSablonParametre { get; set; }
    }
}
