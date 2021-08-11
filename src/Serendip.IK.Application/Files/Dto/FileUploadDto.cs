using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.File.Dto
{
    public class FileUploadDto
    {
        public FileUploadDto()
        {
            DataArray = new byte[0];
        }

        public AccessLevel AccessLevel { get; set; }

        public string FileName { get; set; }

        public IFormFile Data { get; set; }

        public byte[] DataArray { get; set; }

        public string ContentType { get; set; }
    }
}
