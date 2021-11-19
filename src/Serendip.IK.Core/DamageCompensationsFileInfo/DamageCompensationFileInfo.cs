using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsFileInfo
{
    public class DamageCompensationFileInfo :BaseEntity
    {
        public int DamageCompensationId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public string DosyaUzantisi { get; set; }



    }
}
