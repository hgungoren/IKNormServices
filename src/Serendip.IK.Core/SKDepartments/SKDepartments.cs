using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKDepartments
{
    public class SKDepartments : BaseEntity
    {
        public long DepartmanObjId { get; set; }
        public byte[] Sistem_Timestamp { get; set; }
        public string Aciklama { get; set; }
        public string Adi { get; set; }
        public bool?  Aktif { get; set; }
        public bool?  IsOther { get; set; }
        public string ListName { get; set; }
        public long? Sirketi_ObjId { get; set; }
        public string Sistem_InsertLogin { get; set; }
        public DateTime? Sistem_InsertTime { get; set; }
        public string Sistem_TransactionId { get; set; }
        public string Sistem_UpdateLogin { get; set; }
        public DateTime? Sistem_Updatetime { get; set; }
        public string Aciklama1 { get; set; }
        public string ParentKodu { get; set; }
        public string Sistem_UpdateTerminal { get; set; }
        public string Sistem_InsertTerminal { get; set; }
        public int?  SiraNo { get; set; }
        public bool?  BolgeHakki { get; set; }
        public string MeslekKodu { get; set; }
        public string MeslekAdi { get; set; }
        public string BordroKarsiligi { get; set; }
        public long? EksikGunNedeni_ObjId { get; set; }
        public int?  MailSablonParametre { get; set; }
        public long? YoneticiObjId { get; set; }
    }
}
