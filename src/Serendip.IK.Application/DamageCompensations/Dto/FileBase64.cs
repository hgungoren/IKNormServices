using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class FileBase64
    {
        public string name { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public string base64 { get; set; }
        public File file { get; set; }
    }

    public class File
    {
    }
}
