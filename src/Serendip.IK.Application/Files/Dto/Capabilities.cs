using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Files.Dto
{
    public class Capabilities
    {
        public bool CanDelete { get; set; }
        public bool CanRename { get; set; }
        public bool CanCopy { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDownload { get; set; }
        public bool CanListChildren { get; set; }
        public bool CanAddChildren { get; set; }
        public bool CanRemoveChildren { get; set; }
    }
}
