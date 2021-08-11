using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.File.Dto
{
    public class FileUploadResultDto
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }
    }
}
