using System;

namespace Serendip.IK.Notification.Dto
{
    public class Root
    { 
        public int? talepNedeni { get; set; }
        public int? talepTuru { get; set; }
        public string pozisyon { get; set; }
        public long? personelId { get; set; }
        public string aciklama { get; set; }
        public int? normStatus { get; set; }
        public long? subeObjId { get; set; }
        public int? talepDurumu { get; set; }
        public long? bagliOlduguSubeObjId { get; set; }
        public DateTime? creationTime { get; set; }
        public int? id { get; set; }
    }
}
 
