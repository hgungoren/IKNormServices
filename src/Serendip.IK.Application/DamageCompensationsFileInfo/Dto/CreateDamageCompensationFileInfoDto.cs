using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsFileInfo.Dto
{



    [AutoMap(typeof(DamageCompensationFileInfo))]
    public class  CreateDamageCompensationFileInfoDto
    {
        public int DamageCompensationId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public string DosyaUzantisi { get; set; }

        public int DosyaTyp { get; set; }
        public bool DosyaActive { get; set; }



    }
}
