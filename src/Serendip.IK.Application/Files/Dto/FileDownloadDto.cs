using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Files.Dto
{
    public class FileDownloadDto
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}
