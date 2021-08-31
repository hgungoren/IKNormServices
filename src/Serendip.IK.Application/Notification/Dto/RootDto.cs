using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Notification.Dto
{
    public class Root
    {
        public int talepNedeni { get; set; } = 0;
        public int talepTuru { get; set; } = 0;
        public string pozisyon { get; set; } = "Yok";
        public string personelId { get; set; } ="Yok";
        public string aciklama { get; set; } = "Yok";
        public int normStatus { get; set; } = 0;
        public string subeObjId { get; set; } = "Yok";
        public int talepDurumu { get; set; } = 0;
        public long bagliOlduguSubeObjId { get; set; } = 0;
        public DateTime creationTime { get; set; } = DateTime.Now;
        public long id { get; set; } = 0;
    }

}
