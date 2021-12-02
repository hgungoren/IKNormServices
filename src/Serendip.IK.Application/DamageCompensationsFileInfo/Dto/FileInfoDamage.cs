using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsFileInfo.Dto
{
    public class FileInfoDamage
    {


        public string FileTazminDilekcesi { get; set; }
        public string FileFatura { get; set; }
        public string FileSevkirsaliye { get; set; }
        public string FileTcVkno { get; set; }


        public long TazminId { get; set; }
    }
}
